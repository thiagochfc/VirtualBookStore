using System.Net;

using Dapper;

using Newtonsoft.Json.Linq;

using VirtualBookstore.WebApi.Authors.Models;
using VirtualBookstore.WebApi.Books.Models;
using VirtualBookstore.WebApi.Categories.Models;

namespace VirtualBookstore.IntegrationTests.Books;

public class BookTests(VirtualBookstoreWebApplicationFactory applicationFactory) : 
    IClassFixture<VirtualBookstoreWebApplicationFactory>
{
    [Fact(DisplayName = "Should successfully create a new book")]
    public async Task ShouldCreateBookSuccessfully()
    {
        // Arrange
        HttpClient client = applicationFactory.CreateClient();
        AuthorRequestCreate requestAuthor = new AuthorRequestCreate("John Doe", "john.doe@example.com", "I'm an author");
        CreateCategoryRequest requestCategory = new CreateCategoryRequest("Action");
        var responseAuthor = await client.PostAsync(Endpoints.Authors, Utils.CreateRequestAsStringContent(requestAuthor));
        var responseAuthorContent = await responseAuthor.Content.ReadAsStringAsync();
        var author = JObject.Parse(responseAuthorContent);
        var responseCategory = await client.PostAsync(Endpoints.Categories, Utils.CreateRequestAsStringContent(requestCategory));
        var responseCategoryContent = await responseCategory.Content.ReadAsStringAsync();
        var category = JObject.Parse(responseCategoryContent);
        CreateBookRequest request = new("Title",
            "Resume",
            "Summary",
            20.00m,
            100,
            "Isbn",
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            Guid.Parse(category.Value<string>("id")!),
            Guid.Parse(author.Value<string>("id")!));
        
        // Act
        var httpResponse = await client.PostAsync(Endpoints.Books, Utils.CreateRequestAsStringContent(request));
        int countBooksCreated = await applicationFactory
            .Connection
            .QuerySingleAsync<int>("SELECT COUNT(1) FROM books;");

        // Assert
        Assert.True(httpResponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        Assert.Equal(1, countBooksCreated);
    }

    [Fact(DisplayName = "Should not create a new book due to invalid request")]
    public async Task ShouldNotCreateBookDueToInvalidRequest()
    {
        // Arrange
        HttpClient client = applicationFactory.CreateClient();
        CreateBookRequest request = new(string.Empty,
            string.Empty,
            string.Empty,
            19.99m,
            99,
            string.Empty,
            DateOnly.FromDateTime(DateTime.Now),
            Guid.Empty,
            Guid.Empty);
        
        // Act
        var httpResponse = await client.PostAsync(Endpoints.Books, Utils.CreateRequestAsStringContent(request));
        int countBooksCreated = await applicationFactory
            .Connection
            .QuerySingleAsync<int>("SELECT COUNT(1) FROM books;");

        // Assert
        Assert.False(httpResponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        Assert.Equal(0, countBooksCreated);
    }
}
