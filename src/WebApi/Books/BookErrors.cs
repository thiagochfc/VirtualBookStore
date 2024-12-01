namespace VirtualBookstore.WebApi.Books;

public interface IBookError;

public static class BookErrors
{
    public static readonly TitleNotProvidedError TitleNotProvidedError;
    public static readonly ResumeNotProvidedError ResumeNotProvidedError;
    public static readonly ResumeTooLongError ResumeTooLongError;
    public static readonly PriceInvalidError PriceInvalidError;
    public static readonly NumberOfPagesInvalidError NumberOfPagesInvalidError;
    public static readonly IsbnNotProvidedError IsbnNotProvidedError;
    public static readonly ReleaseInvalidError ReleaseInvalidError;
    public static readonly IdCategoryNotProvidedError IdCategoryNotProvidedError;
    public static readonly IdAuthorNotProvidedError IdAuthorNotProvidedError;
}

public readonly struct TitleNotProvidedError : IBookError;
public readonly struct ResumeNotProvidedError : IBookError;
public readonly struct ResumeTooLongError : IBookError;
public readonly struct PriceInvalidError : IBookError;
public readonly struct NumberOfPagesInvalidError : IBookError;
public readonly struct IsbnNotProvidedError : IBookError;
public readonly struct ReleaseInvalidError : IBookError;
public readonly struct IdCategoryNotProvidedError : IBookError;
public readonly struct IdAuthorNotProvidedError : IBookError;
