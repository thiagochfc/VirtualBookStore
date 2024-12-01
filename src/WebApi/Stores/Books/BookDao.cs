namespace VirtualBookstore.WebApi.Stores.Books;

public record class BookDao(Guid Id,
    string Title,
    string Resume,
    string Summary,
    decimal Price,
    uint NumberOfPages,
    string Isbn,
    DateOnly Release,
    Guid CategoryId,
    Guid AuthorId);
