namespace VirtualBookstore.WebApi.Books.Models;

public record CreateBookRequest(string Title,
    string Resume,
    string Summary,
    decimal Price,
    uint NumberOfPages,
    string Isbn,
    DateOnly Release,
    Guid CategoryId,
    Guid AuthorId);
