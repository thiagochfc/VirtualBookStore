using Moonad;

namespace VirtualBookstore.WebApi.Books;

public interface IBookStore
{
    Task<Option<Book>> GetBookByTitleAsync(string title, CancellationToken cancellationToken);
    Task<Option<Book>> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken);
    Task<Result> CreateAsync(Book book, CancellationToken cancellationToken);
}
