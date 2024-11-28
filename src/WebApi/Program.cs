using VirtualBookstore.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddDocumentation();
builder.AddDatabase();
builder.AddValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigureDevelopmentEnvironment();
}

app.UseSecurity();
app.MapEndpoints();

await app
    .RunAsync()
    .ConfigureAwait(false);
