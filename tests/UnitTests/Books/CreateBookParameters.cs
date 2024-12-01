using System.Collections;

using VirtualBookstore.WebApi.Books;

namespace VirtualBookstore.UnitTests.Books;

public class CreateBookParameters  : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return TitleNotProvided();
        yield return ResumeNotProvided();
        yield return ResumeTooLong();
        yield return PriceInvalid();
        yield return NumberOfPagesInvalid();
        yield return IsbnNotProvided();
        yield return ReleaseInvalid();
        yield return IdCategoryNotProvided();
        yield return IdAuthorNotProvided();
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private static object[] TitleNotProvided() => 
    [
        string.Empty,
        Constants.Resume,
        Constants.Summary,
        Constants.Price,
        Constants.NumberOfPages,
        Constants.Isbn,
        Constants.Release,
        Constants.IdCategory,
        Constants.IdAuthor,
        BookErrors.TitleNotProvidedError,
    ];

    private static object[] ResumeNotProvided() =>
    [
        Constants.Title,
        string.Empty,
        Constants.Summary,
        Constants.Price,
        Constants.NumberOfPages,
        Constants.Isbn,
        Constants.Release,
        Constants.IdCategory,
        Constants.IdAuthor,
        BookErrors.ResumeNotProvidedError,
    ];
    
    private static object[] ResumeTooLong() =>
    [
        Constants.Title,
        new string('a', Book.ResumeMaxLength + 1),
        Constants.Summary,
        Constants.Price,
        Constants.NumberOfPages,
        Constants.Isbn,
        Constants.Release,
        Constants.IdCategory,
        Constants.IdAuthor,
        BookErrors.ResumeTooLongError,
    ];
    
    private static object[] PriceInvalid() =>
    [
        Constants.Title,
        Constants.Resume,
        Constants.Summary,
        Book.MinimumPriceRequired - 0.01m,
        Constants.NumberOfPages,
        Constants.Isbn,
        Constants.Release,
        Constants.IdCategory,
        Constants.IdAuthor,
        BookErrors.PriceInvalidError,
    ];
    
    private static object[] NumberOfPagesInvalid() =>
    [
        Constants.Title,
        Constants.Resume,
        Constants.Summary,
        Constants.Price,
        Book.MinimumNumberOfPages - 1,
        Constants.Isbn,
        Constants.Release,
        Constants.IdCategory,
        Constants.IdAuthor,
        BookErrors.NumberOfPagesInvalidError,
    ];
    
    private static object[] IsbnNotProvided() =>
    [
        Constants.Title,
        Constants.Resume,
        Constants.Summary,
        Constants.Price,
        Constants.NumberOfPages,
        string.Empty,
        Constants.Release,
        Constants.IdCategory,
        Constants.IdAuthor,
        BookErrors.IsbnNotProvidedError,
    ];
    
    private static object[] ReleaseInvalid() =>
    [
        Constants.Title,
        Constants.Resume,
        Constants.Summary,
        Constants.Price,
        Constants.NumberOfPages,
        Constants.Isbn,
        DateOnly.FromDateTime(DateTime.Now),
        Constants.IdCategory,
        Constants.IdAuthor,
        BookErrors.ReleaseInvalidError,
    ];
    
    private static object[] IdCategoryNotProvided() =>
    [
        Constants.Title,
        Constants.Resume,
        Constants.Summary,
        Constants.Price,
        Constants.NumberOfPages,
        Constants.Isbn,
        Constants.Release,
        Guid.Empty,
        Constants.IdAuthor,
        BookErrors.IdCategoryNotProvidedError,
    ];
    
    private static object[] IdAuthorNotProvided() =>
    [
        Constants.Title,
        Constants.Resume,
        Constants.Summary,
        Constants.Price,
        Constants.NumberOfPages,
        Constants.Isbn,
        Constants.Release,
        Constants.IdCategory,
        Guid.Empty,
        BookErrors.IdAuthorNotProvidedError,
    ];
}
