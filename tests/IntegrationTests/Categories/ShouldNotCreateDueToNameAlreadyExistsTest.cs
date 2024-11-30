using Dapper;

using VirtualBookstore.WebApi.Categories.Models;

namespace VirtualBookstore.IntegrationTests.Categories;

public class ShouldNotCreateDueToNameAlreadyExistsTest(VirtualBookstoreWebApplicationFactory applicationFactory) : 
    IClassFixture<VirtualBookstoreWebApplicationFactory>
{
    [Fact(DisplayName = "Should not create a new category due to name already exists")]
    public async Task ShouldNotCreateDueToNameAlreadyExists()
    {
        // Arrange
        HttpClient client = applicationFactory.CreateClient();
        CreateCategoryRequest request = new("Action");
        
        // Act
        await Utils.ExecuteConcurrently(10, async (cancellationToken) =>
        { 
            await client.PostAsync(Endpoints.Categories, 
                Utils.CreateRequestAsStringContent(request), 
                cancellationToken);
        });
        int countCategoriesCreated = await applicationFactory
            .Connection
            .QuerySingleAsync<int>("SELECT COUNT(1) FROM categories;");
        
        // Assert
        Assert.Equal(1, countCategoriesCreated);
    }
}
