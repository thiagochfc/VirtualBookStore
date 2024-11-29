using System.Data.Common;
using System.Net.Mime;
using System.Text;

using DotNet.Testcontainers.Builders;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using Testcontainers.PostgreSql;

namespace VirtualBookstore.IntegrationTests;

public class VirtualBookstoreWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:17.2-alpine3.20")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
        .Build();

    public DbConnection Connection { get; private set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:postgresql", _postgreSqlContainer.GetConnectionString());
    }
    
    public async Task InitializeAsync()
    {
         await _postgreSqlContainer
             .StartAsync()
             .ConfigureAwait(false);
         
         Connection = Services.CreateScope().ServiceProvider.GetRequiredService<DbConnection>();
    }

    public new async Task DisposeAsync()
    {
        await _postgreSqlContainer
            .StopAsync()
            .ConfigureAwait(false);
    }
}
