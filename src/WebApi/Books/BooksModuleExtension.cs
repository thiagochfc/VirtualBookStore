using VirtualBookstore.WebApi.Stores.Books;

namespace VirtualBookstore.WebApi.Books;

public static class BooksModuleExtension
{
    internal static void AddBooksModule(this WebApplicationBuilder builder) =>
        builder.Services.AddScoped<IBookStore, BookStore>();
}
