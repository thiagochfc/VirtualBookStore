using System.Net;

using Dapper;

using Newtonsoft.Json;

using VirtualBookstore.WebApi.Authors.Models;

namespace VirtualBookstore.IntegrationTests.Authors;

public class AuthorTests(VirtualBookstoreWebApplicationFactory applicationFactory) : IClassFixture<VirtualBookstoreWebApplicationFactory>
{
    [Fact(DisplayName = "Should successfully create a new author")]
    public async Task ShouldCreateAuthorSuccessfully()
    {
        // Arrange
        HttpClient client = applicationFactory.CreateClient();
        AuthorRequestCreate request = new("Jonh Doe", "jonh.doe@gmail.com", "I'm a author"); 
        
        // Act
        var httpResponse = await client.PostAsync(Endpoints.Authors, Utils.CreateRequestAsStringContent(request));
        string body = await httpResponse.Content.ReadAsStringAsync();
        CreateAuthorResponse response = JsonConvert.DeserializeObject<CreateAuthorResponse>(body)!;
        int countAuthorsCreated = await applicationFactory.Connection.QuerySingleAsync<int>("SELECT COUNT(1) FROM authors;");

        // Assert
        Assert.True(httpResponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Email, response.Email);
        Assert.Equal(request.Description, response.Description);
        Assert.Equal(1, countAuthorsCreated);
    }

    [Fact(DisplayName = "Should not create a new author due to invalid request")]
    public async Task ShouldNotCreateAuthorDueToInvalidRequest()
    {
        // Arrange
        HttpClient client = applicationFactory.CreateClient();
        AuthorRequestCreate request = new(string.Empty, string.Empty, string.Empty);
        
        // Act
        var httpResponse = await client.PostAsync(Endpoints.Authors, Utils.CreateRequestAsStringContent(request));
        int countAuthorsCreated = await applicationFactory.Connection.QuerySingleAsync<int>("SELECT COUNT(1) FROM authors;");
        
        // Assert
        Assert.False(httpResponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        Assert.Equal(0, countAuthorsCreated);
    }
}
