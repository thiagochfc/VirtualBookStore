using VirtualBookstore.WebApi.Authors;

namespace VirtualBookstore.UnitTests.Authors;

public class AuthorTests
{
    [Fact(DisplayName = "Should successfully create a new author")]
    public void ShouldCreateAuthorSuccessfully()
    {
        // Act
        var authorResult = Author.Create(Constants.Name, Constants.Email, Constants.Description);

        // Assert
        Assert.True(authorResult.IsOk);
        Assert.NotEqual(Guid.Empty, authorResult.ResultValue.Id);
        Assert.NotEqual(default, authorResult.ResultValue.Registered);
    }

    [Theory(DisplayName = "Should not create a new author due to invalid parameters")]
    [ClassData(typeof(CreateAuthorParameters))]
    public void ShouldNotCreateAuthorDueInvalidParameters(string name,
        string email,
        string description, 
        IAuthorError expectedError)
    {
        // Act
        var authorResult = Author.Create(name, email, description);
        
        // Assert
        Assert.True(authorResult.IsError);
        Assert.Equal(expectedError, authorResult.ErrorValue);
    }
}
