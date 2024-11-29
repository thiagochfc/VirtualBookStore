using System.Reflection;

using DbUp;
using DbUp.Engine;

using FluentValidation;

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

        UpgradeEngine upgrader = DeployChanges
            .To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        upgrader.PerformUpgrade();
    }

    internal static void AddValidation(this WebApplicationBuilder builder) =>
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}
