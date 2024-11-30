using VirtualBookstore.WebApi.Authors;
using VirtualBookstore.WebApi.Categories;
using VirtualBookstore.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddDocumentation();
builder.AddDatabase();
builder.AddValidation();
builder.AddAuthorsModule();
builder.AddCategoriesModule();

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

public partial class Program
{
    protected Program()  { }
}
