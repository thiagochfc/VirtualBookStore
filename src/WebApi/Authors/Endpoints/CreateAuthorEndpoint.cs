using Moonad;

using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

using VirtualBookstore.WebApi.Authors.Models;
using VirtualBookstore.WebApi.Commons;

namespace VirtualBookstore.WebApi.Authors.Endpoints;

public class CreateAuthorEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app
            .MapPost("/", HandleAsync)
            .WithName("Author: Create")
            .WithSummary("Create a new Author")
            .WithDescription("Create a new Author")
            .AddFluentValidationAutoValidation();

    private static async Task<IResult> HandleAsync(
        AuthorRequestCreate request,
        IAuthorStore authorStore,
        CancellationToken cancellationToken)
    {
        Result<Author, IAuthorError> authorResult = Author.Create(request.Name, request.Email, request.Description);

        if (authorResult.IsError)
        {
            return authorResult.ErrorValue switch
            {
                NameNotProvidedError => TypedResults.BadRequest("Author's name is not provided"),
                EmailNotProvidedError => TypedResults.BadRequest("Author's email is not provided"),
                EmailInvalidError => TypedResults.BadRequest("Author's email is not valid"),
                DescriptionNotProvidedError => TypedResults.BadRequest("Author's description is not provided"),
                DescriptionTooLongError => TypedResults.BadRequest($"Author's description is too long. Max length is {Author.DescriptionMaxLength}"),
                _ => TypedResults.InternalServerError("An unknown error occured")
            };
        }

        Author author = authorResult.ResultValue;
        Result authorStoreResult = await authorStore
            .CreateAsync(author, cancellationToken)
            .ConfigureAwait(false);
        
        return authorStoreResult.IsOk 
            ? TypedResults.Created($"/author/{author.Id}", CreateAuthorResponse.From(author))
            : TypedResults.InternalServerError("An unknown error occured");
    }
}
