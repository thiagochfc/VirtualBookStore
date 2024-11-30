using VirtualBookstore.WebApi.Stores;
using VirtualBookstore.WebApi.Stores.Categories;

namespace VirtualBookstore.WebApi.Categories;

internal static class CategoriesModuleExtension
{
    internal static void AddCategoriesModule(this WebApplicationBuilder builder) =>
        builder.Services.AddScoped<ICategoryStore, CategoryStore>();
}
