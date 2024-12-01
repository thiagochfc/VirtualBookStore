using VirtualBookstore.WebApi.Books;

namespace VirtualBookstore.UnitTests.Books;

public class BookTests
{
    [Fact(DisplayName = "Should successfully create a new book")]
    public void ShouldCreateBookSuccessfully()
    {
        // Act
        var bookResult = Book.Create(Constants.Title,
            Constants.Resume,
            Constants.Summary,
            Constants.Price,
            Constants.NumberOfPages,
            Constants.Isbn,
            Constants.Release,
            Constants.IdCategory,
            Constants.IdAuthor);
        Book book = bookResult.ResultValue;
        
        // Assert
        Assert.True(bookResult);
        Assert.NotEqual(Guid.Empty, book.Id);
    }

    [Theory(DisplayName = "Should not create a new book due to invalid parameters")]
    [ClassData(typeof(CreateBookParameters))]
    public void ShouldNotCreateBookDueToInvalidParameters(string title,
        string resume,
        string summary,
        decimal price,
        uint numberOfPages,
        string isbn,
        DateOnly release,
        Guid idCategory,
        Guid idAuthor,
        IBookError expectedError)
    {
        // Act
        var bookResult = Book.Create(title,
            resume,
            summary,
            price,
            numberOfPages,
            isbn,
            release,
            idCategory,
            idAuthor);
        
        // Assert
        Assert.True(bookResult.IsError);
        Assert.Equal(expectedError, bookResult.ErrorValue);
    }
}
