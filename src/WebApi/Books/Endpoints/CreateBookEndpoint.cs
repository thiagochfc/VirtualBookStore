using Moonad;

using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

using VirtualBookstore.WebApi.Authors;
using VirtualBookstore.WebApi.Books.Models;
using VirtualBookstore.WebApi.Categories;
using VirtualBookstore.WebApi.Commons;
using VirtualBookstore.WebApi.Stores.Books;

namespace VirtualBookstore.WebApi.Books.Endpoints;

public class CreateBookEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app
            .MapPost("/", HandleAsync)
            .WithName("Book: Create")
            .WithSummary("Create a new Book")
            .WithDescription("Create a new Book")
            .AddFluentValidationAutoValidation();

    private static async Task<IResult> HandleAsync(CreateBookRequest request,
        IBookStore bookStore,
        CancellationToken cancellationToken)
    {
        var bookResult = Book.Create(request.Title,
            request.Resume,
            request.Summary,
            request.Price,
            request.NumberOfPages,
            request.Isbn,
            request.Release,
            request.CategoryId,
            request.AuthorId);

        if (bookResult.IsError)
        {
            return bookResult.ErrorValue switch
            {
                _ => TypedResults.InternalServerError("An unknown error occured")
            };
        }

        Book book = bookResult.ResultValue;
        Result bookStoreResult = await bookStore
            .CreateAsync(book, cancellationToken)
            .ConfigureAwait(false);
        
        return bookStoreResult.IsOk
            ? Results.Created($"/books/{book.Id}", book)
            : Results.InternalServerError("An unknown error occured");
    }
}
