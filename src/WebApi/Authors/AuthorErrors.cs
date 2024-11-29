namespace VirtualBookstore.WebApi.Authors;

public interface IAuthorError {} 

public static class AuthorErrors
{
    public static readonly NameNotProvidedError NameNotProvidedError;
    public static readonly EmailNotProvidedError EmailNotProvidedError;
    public static readonly EmailInvalidError EmailInvalidError;
    public static readonly DescriptionNotProvidedError DescriptionNotProvidedError;
    public static readonly DescriptionTooLongError DescriptionTooLongError;
}

public readonly struct NameNotProvidedError : IAuthorError {}
public readonly struct EmailNotProvidedError : IAuthorError {}
public readonly struct EmailInvalidError : IAuthorError { }
public readonly struct DescriptionNotProvidedError : IAuthorError {}
public readonly struct DescriptionTooLongError : IAuthorError {}
