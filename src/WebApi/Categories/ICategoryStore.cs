using Moonad;

namespace VirtualBookstore.WebApi.Categories;

public interface ICategoryStore
{
    Task<Option<Category>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Option<Category>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Result> CreateAsync(Category category, CancellationToken cancellationToken);
}
