using VirtualBookstore.WebApi.Stores.Authors;

namespace VirtualBookstore.WebApi.Authors;

internal static class AuthorsModuleExtension
{
    internal static void AddAuthorsModule(this WebApplicationBuilder builder) =>
        builder.Services.AddScoped<IAuthorStore, AuthorStore>();
}
