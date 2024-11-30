using System.Data.Common;

using Dapper;

using Moonad;

using VirtualBookstore.WebApi.Categories;

namespace VirtualBookstore.WebApi.Stores.Categories;

public sealed class CategoryStore(DbConnection connection) : StoreBase, ICategoryStore
{
    public async Task<Option<Category>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        const string sql = "SELECT Id, Name FROM categories WHERE Name = @Name;";
        
        CategoryDao? categoryDao = await connection
            .QuerySingleOrDefaultAsync<CategoryDao>(sql, new { Name = name })
            .ConfigureAwait(false);

        if (categoryDao is null)
        {
            return Option.None<Category>();
        }

        return Load<Category>(categoryDao.Id, categoryDao.Name)
            .ToOption();
    }

    public async Task<Result> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        const string sql =
            """
            INSERT INTO categories (id, name) 
            VALUES (@Id, @Name)
            """;
        
        await connection
            .ExecuteAsync(sql, new
            {
                Id = category.Id,
                Name = category.Name
            })
            .ConfigureAwait(false);

        return Result.Ok();
    }
}
