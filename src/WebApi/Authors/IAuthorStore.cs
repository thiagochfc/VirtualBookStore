using Moonad;

namespace VirtualBookstore.WebApi.Authors;

public interface IAuthorStore
{
    public Task<Option<Author>> GetByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<Result> CreateAsync(Author author, CancellationToken cancellationToken);
}
