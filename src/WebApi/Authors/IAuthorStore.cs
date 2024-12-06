using Moonad;

namespace VirtualBookstore.WebApi.Authors;

public interface IAuthorStore
{
    Task<Option<Author>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Option<Author>> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Result> CreateAsync(Author author, CancellationToken cancellationToken);
}
