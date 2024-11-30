using System.Net;

using Dapper;

using Microsoft.Extensions.DependencyInjection;

using VirtualBookstore.WebApi.Categories.Models;

namespace VirtualBookstore.IntegrationTests.Categories;

public class CategoryTests(VirtualBookstoreWebApplicationFactory applicationFactory) : IClassFixture<VirtualBookstoreWebApplicationFactory>
{
    [Fact(DisplayName = "Should successfully create a new category")]
    public async Task ShouldCreateCategorySuccessfully()
    {
        // Arrange
        HttpClient client = applicationFactory.CreateClient();
        CreateCategoryRequest request = new("Action");
        
        // Act
        var httpResponse = await client.PostAsync(Endpoints.Categories, Utils.CreateRequestAsStringContent(request));
        int countCategoriesCreated = await applicationFactory
            .Connection
            .QuerySingleAsync<int>("SELECT COUNT(1) FROM categories;");
        
        // Assert
        Assert.True(httpResponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        Assert.Equal(1, countCategoriesCreated);
    }

    [Fact(DisplayName = "Should not create a new category due to invalid request")]
    public async Task ShouldNotCreateCategoryDueToInvalidRequest()
    {
        // Arrange
        HttpClient client = applicationFactory.CreateClient();
        CreateCategoryRequest request = new(string.Empty);
        
        // Act
        var httpResponse = await client.PostAsync(Endpoints.Categories, Utils.CreateRequestAsStringContent(request));
        int countCategoriesCreated = await applicationFactory
            .Connection
            .QuerySingleAsync<int>("SELECT COUNT(1) FROM categories;");
        
        // Assert
        Assert.False(httpResponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        Assert.Equal(0, countCategoriesCreated);
    }
}
