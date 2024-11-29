using Dapper;

using VirtualBookstore.WebApi.Authors.Models;

using Xunit.Abstractions;

namespace VirtualBookstore.IntegrationTests.Authors;

public class ShouldNotCreateDueToEmailAlreadyExistsTest(VirtualBookstoreWebApplicationFactory applicationFactory) : 
    IClassFixture<VirtualBookstoreWebApplicationFactory>
{
    private const string Endpoint = "/authors";
    
    [Fact(DisplayName = "Should not create a new author due to email already exists")]
    public async Task ShouldNotCreateDueToEmailAlreadyExists()
    {
        // Arrange
        var client = applicationFactory.CreateClient();
        AuthorRequestCreate request = new("Jonh Doe", "jonh.doe@gmail.com", "I'm a author");
        
        // Act
        await Utils.ExecuteConcurrently(10, async (cancellationToken) =>
        { 
            await client.PostAsync(Endpoint,
                Utils.CreateRequestAsStringContent(request), cancellationToken);
        });
        int countAuthorsCreated = await applicationFactory.Connection.QuerySingleAsync<int>("SELECT COUNT(1) FROM authors;");
        

        // Assert
        Assert.Equal(1, countAuthorsCreated);
    }
}
