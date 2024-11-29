using System.Collections;

using VirtualBookstore.WebApi.Authors;

namespace VirtualBookstore.UnitTests.Authors;

public class CreateAuthorParameters : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return NameNotProvided();
        yield return EmailNotProvided();
        yield return EmailInvalid();
        yield return DescriptionNotProvided();
        yield return DescriptionTooLong();
    }
    
    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private static object[] NameNotProvided() => 
    [
        string.Empty,
        Constants.Email,
        Constants.Description,
        AuthorErrors.NameNotProvidedError,
    ];

    private static object[] EmailNotProvided() =>
    [
        Constants.Name,
        string.Empty,
        Constants.Description,
        AuthorErrors.EmailNotProvidedError,
    ];
    
    private static object[] EmailInvalid() =>
    [
        Constants.Name,
        "a.com",
        Constants.Description,
        AuthorErrors.EmailInvalidError,
    ];
    
    private static object[] DescriptionNotProvided() =>
    [
        Constants.Name,
        Constants.Email,
        string.Empty,
        AuthorErrors.DescriptionNotProvidedError,
    ];
    
    private static object[] DescriptionTooLong() =>
    [
        Constants.Name,
        Constants.Email,
        new string('a', Author.DescriptionMaxLength + 1),
        AuthorErrors.DescriptionTooLongError,
    ];
}
