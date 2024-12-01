using Moonad;

namespace VirtualBookstore.WebApi.Books;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Resume { get; private set; }
    public Option<string> Summary { get; private set; }
    public decimal Price { get; private set; }
    public uint NumberOfPages { get; private set; }
    public string Isbn { get; private set; }
    public DateOnly Release { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid AuthorId { get; private set; }
    public static ushort ResumeMaxLength => 500;
    public static decimal MinimumPriceRequired => 20.00m;
    public static sbyte MinimumNumberOfPages => 100;

    private Book(Guid id,
        string title,
        string resume,
        Option<string> summary,
        decimal price,
        uint numberOfPages,
        string isbn,
        DateOnly release,
        Guid categoryId,
        Guid authorId) =>
        (Id, Title, Resume, Summary, Price, NumberOfPages, Isbn, Release, CategoryId, AuthorId) =
        (id, title, resume, summary, price, numberOfPages, isbn, release, categoryId, authorId);
    
    public static Result<Book, IBookError> Create(string title,
        string resume,
        string summary,
        decimal price,
        uint numberOfPages,
        string isbn,
        DateOnly release,
        Guid categoryId,
        Guid authorId)
    {
        if (string.IsNullOrEmpty(title))
        {
            return BookErrors.TitleNotProvidedError;
        }

        if (string.IsNullOrEmpty(resume))
        {
            return BookErrors.ResumeNotProvidedError;
        }

        if (resume.Length > ResumeMaxLength)
        {
            return BookErrors.ResumeTooLongError;
        }

        if (price < MinimumPriceRequired)
        {
            return BookErrors.PriceInvalidError;
        }

        if (numberOfPages < MinimumNumberOfPages)
        {
            return BookErrors.NumberOfPagesInvalidError;
        }

        if (string.IsNullOrEmpty(isbn))
        {
            return BookErrors.IsbnNotProvidedError;
        }

        if (release <= DateOnly.FromDateTime(DateTime.Now))
        {
            return BookErrors.ReleaseInvalidError;
        }

        if (categoryId.Equals(Guid.Empty))
        {
            return BookErrors.IdCategoryNotProvidedError;
        }

        if (authorId.Equals(Guid.Empty))
        {
            return BookErrors.IdAuthorNotProvidedError;
        }
        
        Book book = new (Guid.CreateVersion7(),
            title,
            resume,
            summary,
            price,
            numberOfPages,
            isbn,
            release,
            categoryId,
            authorId);
        return book;
    }
    
}
