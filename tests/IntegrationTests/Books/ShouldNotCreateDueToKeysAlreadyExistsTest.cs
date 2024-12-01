using Dapper;

using Newtonsoft.Json.Linq;

using VirtualBookstore.WebApi.Authors.Models;
using VirtualBookstore.WebApi.Books.Models;
using VirtualBookstore.WebApi.Categories.Models;

namespace VirtualBookstore.IntegrationTests.Books;

public class ShouldNotCreateDueToKeysAlreadyExistsTest(VirtualBookstoreWebApplicationFactory applicationFactory) : 
    IClassFixture<VirtualBookstoreWebApplicationFactory>
{
    [Fact(DisplayName = "Should not create a new book due to keys already exists")]
    public async Task ShouldNotCreateDueToKeysAlreadyExists()
    {
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
        await Utils.ExecuteConcurrently(10, async (cancellationToken) =>
        { 
            await client
                .PostAsync(Endpoints.Books, 
                    Utils.CreateRequestAsStringContent(request), 
                    cancellationToken);
        });
        int countBooksCreated = await applicationFactory
            .Connection
            .QuerySingleAsync<int>("SELECT COUNT(1) FROM authors;");
        
        // Assert
        Assert.Equal(1, countBooksCreated);
    }
}
