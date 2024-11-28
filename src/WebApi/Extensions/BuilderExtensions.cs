namespace VirtualBookstore.WebApi.Extensions;

static class BuilderExtensions
{
    internal static void AddDocumentation(this WebApplicationBuilder builder) =>
        builder.Services.AddOpenApi();

    internal static void AddDatabase(this WebApplicationBuilder builder)
    {
        string connectionString =
            builder.Configuration.GetConnectionString("postgresql") ?? throw new InvalidDataException("PostgreSQL connection string not found");
        builder.Services.AddNpgsqlDataSource(connectionString);
    }
}
