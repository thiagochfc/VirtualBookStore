using Moonad;

using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

using VirtualBookstore.WebApi.Categories.Models;
using VirtualBookstore.WebApi.Commons;

namespace VirtualBookstore.WebApi.Categories.Endpoints;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app
            .MapPost("/", HandleAsync)
            .WithName("Category: Create")
            .WithSummary("Create a new Category")
            .WithDescription("Create a new Category")
            .AddFluentValidationAutoValidation();

    private static async Task<IResult> HandleAsync(CreateCategoryRequest request,
        ICategoryStore categoryStore,
        CancellationToken cancellationToken)
    {
        var categoryResult = Category.Create(request.Name);

        if (categoryResult.IsError)
        {
            return categoryResult.ErrorValue switch
            {
                NameNotProvidedError => TypedResults.BadRequest("Category's name is not provided"),
                _ => TypedResults.InternalServerError("An unknown error occured")
            };
        }
        
        Category category = categoryResult.ResultValue;
        Result categoryStoreResult = await categoryStore
            .CreateAsync(category, cancellationToken)
            .ConfigureAwait(false);
        
        return categoryStoreResult.IsOk
            ? TypedResults.Created($"/category/{category.Id}", category)
            : TypedResults.InternalServerError("An unknown error occured");
    }
}
