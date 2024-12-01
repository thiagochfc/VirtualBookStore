using System.Data.Common;
using System.Globalization;

using Dapper;

using Moonad;

using VirtualBookstore.WebApi.Books;

namespace VirtualBookstore.WebApi.Stores.Books;

public sealed class BookStore(DbConnection connection) : StoreBase, IBookStore
{
    public async Task<Option<Book>> GetBookByTitleAsync(string title, CancellationToken cancellationToken)
    {
        const string sql = """
                           SELECT id
                                ,title
                                ,resume
                                ,summary
                                ,price
                                ,number_of_pages as NumberOfPages
                                ,isbn
                                ,release
                                ,category_id as CategoryId 
                                ,author_id as AuthorId
                           FROM books WHERE title = @Title
                           """;

        BookDao? bookDao = await connection
            .QuerySingleOrDefaultAsync<BookDao>(sql, new { Title = title })
            .ConfigureAwait(false);
        
        if (bookDao is null)
        {
            return Option.None<Book>();
        }

        return Load<Book>(bookDao.Id,
                bookDao.Title,
                bookDao.Resume,
                bookDao.Summary,
                bookDao.Price,
                bookDao.NumberOfPages,
                bookDao.Isbn,
                bookDao.Release,
                bookDao.CategoryId,
                bookDao.AuthorId)
            .ToOption();
    }

    public async Task<Option<Book>> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken)
    {
        const string sql = """
                           SELECT id
                                ,title
                                ,resume
                                ,summary
                                ,price
                                ,number_of_pages as NumberOfPages
                                ,isbn
                                ,release
                                ,category_id as CategoryId 
                                ,author_id as AuthorId
                           FROM books WHERE isbn = @Isbn
                           """;

        BookDao? bookDao = await connection
            .QuerySingleOrDefaultAsync<BookDao>(sql, new { Isbn = isbn })
            .ConfigureAwait(false);
        
        if (bookDao is null)
        {
            return Option.None<Book>();
        }

        return Load<Book>(bookDao.Id,
                bookDao.Title,
                bookDao.Resume,
                bookDao.Summary,
                bookDao.Price,
                bookDao.NumberOfPages,
                bookDao.Isbn,
                bookDao.Release,
                bookDao.CategoryId,
                bookDao.AuthorId)
            .ToOption();
    }

    public async Task<Result> CreateAsync(Book book, CancellationToken cancellationToken)
    {
        const string sql = """
                           INSERT INTO books(id
                            ,title
                            ,resume
                            ,summary
                            ,price
                            ,number_of_pages
                            ,isbn
                            ,release
                            ,category_id 
                            ,author_id)
                           VALUES (
                             @Id
                            ,@Title
                            ,@Resume
                            ,@Summary
                            ,@Price
                            ,@NumberOfPages
                            ,@Isbn
                            ,@Release
                            ,@CategoryId
                            ,@AuthorId
                           );
                           """;
        
        await connection
            .ExecuteAsync(sql,new
            {
                Id = book.Id,
                Title = book.Title,
                Resume = book.Resume,
                Summary = book.Summary.Get(),
                Price = book.Price,
                NumberOfPages = (short)book.NumberOfPages,
                Isbn = book.Isbn,
                Release = book.Release.ToDateTime(new TimeOnly(0, 0)),
                CategoryId = book.CategoryId,
                AuthorId = book.AuthorId
            })
            .ConfigureAwait(false);
        
        return Result.Ok();
    }
}
