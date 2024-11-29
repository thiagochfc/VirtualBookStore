using Microsoft.AspNetCore.Authentication;

using Moonad;

namespace VirtualBookstore.WebApi.Authors;

public class Author
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public DateTime Registered { get; private set; }
    public static int DescriptionMaxLength => 400;

    private Author(Guid id, string name, string email, string description, DateTime registered) =>
        (Id, Name, Email, Description, Registered) = (id, name, email, description, registered);

    public static Result<Author, IAuthorError> Create(string name, string email, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            return AuthorErrors.NameNotProvidedError;
        }

        if (string.IsNullOrEmpty(email))
        {
            return AuthorErrors.EmailNotProvidedError;
        }

        if (!email.Contains('@', StringComparison.InvariantCultureIgnoreCase))
        {
            return AuthorErrors.EmailInvalidError;
        }

        if (string.IsNullOrEmpty(description))
        {
            return AuthorErrors.DescriptionNotProvidedError;
        }

        if (description.Length > DescriptionMaxLength)
        {
            return AuthorErrors.DescriptionTooLongError;
        }
        
        Author author = new(Guid.CreateVersion7(), name, email, description, DateTime.Now);
        return author;
    }
}
