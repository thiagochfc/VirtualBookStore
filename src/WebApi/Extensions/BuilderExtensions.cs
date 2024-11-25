namespace VirtualBookstore.WebApi.Extensions;

static class BuilderExtensions
{
    internal static void AddDocumentation(this WebApplicationBuilder builder) =>
        builder.Services.AddOpenApi();
}
