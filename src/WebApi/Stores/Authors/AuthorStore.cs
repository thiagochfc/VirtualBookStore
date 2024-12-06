using System.Data.Common;

using Dapper;

using Moonad;

using VirtualBookstore.WebApi.Authors;

namespace VirtualBookstore.WebApi.Stores.Authors;

internal sealed class AuthorStore(DbConnection connection) : StoreBase, IAuthorStore
{
    public async Task<Option<Author>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT Id, Name, Email, Description, Registered FROM authors WHERE id = @Id;";

        AuthorDao? authorDao = await connection
            .QuerySingleOrDefaultAsync<AuthorDao>(sql, new { Id = id })
            .ConfigureAwait(false);

        if (authorDao is null)
        {
            return Option.None<Author>();
        }

        return Load<Author>(authorDao.Id,
                authorDao.Name,
                authorDao.Email,
                authorDao.Description,
                authorDao.Registered)
            .ToOption();
    }

    public async Task<Option<Author>> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        const string sql = "SELECT Id, Name, Email, Description, Registered FROM authors WHERE email = @Email;";

        AuthorDao? authorDao = await connection
            .QuerySingleOrDefaultAsync<AuthorDao>(sql, new { Email = email })
            .ConfigureAwait(false);

        if (authorDao is null)
        {
            return Option.None<Author>();
        }

        return Load<Author>(authorDao.Id,
            authorDao.Name,
            authorDao.Email,
            authorDao.Description,
            authorDao.Registered)
            .ToOption();
    }

    public async Task<Result> CreateAsync(Author author, CancellationToken cancellationToken)
    {
        const string sql = 
            """
            INSERT INTO authors (id, name, email, description, registered)
            VALUES (@Id, @Name, @Email, @Description, @Registered);
            """;
        
        await connection
            .ExecuteAsync(sql, new
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
                Description = author.Description,
                Registered = author.Registered
            })
            .ConfigureAwait(false);
        
        return Result.Ok();
    }
}
