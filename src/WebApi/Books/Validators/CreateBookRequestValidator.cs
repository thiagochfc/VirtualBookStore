using FluentValidation;

using Moonad;

using VirtualBookstore.WebApi.Authors;
using VirtualBookstore.WebApi.Books.Models;
using VirtualBookstore.WebApi.Categories;

namespace VirtualBookstore.WebApi.Books.Validators;

public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
{
    private readonly IBookStore _bookStore;
    private readonly IAuthorStore _authorStore;
    private readonly ICategoryStore _categoryStore;
    public CreateBookRequestValidator(IBookStore bookStore,
        IAuthorStore authorStore,
        ICategoryStore categoryStore)
    {
        _bookStore = bookStore;
        _authorStore = authorStore;
        _categoryStore = categoryStore;
        
        // Title
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50)
            .MustAsync(IsTitleUniqueAsync)
            .WithMessage("Title already exists");
        
        // Resume
        RuleFor(x => x.Resume)
            .NotEmpty()
            .MaximumLength(500);

        // Price
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(20.00m);
        
        // Number of Pages
        RuleFor(x => x.NumberOfPages)
            .GreaterThanOrEqualTo((uint)100);
        
        // Isbn
        RuleFor(x => x.Isbn)
            .MaximumLength(13)
            .MustAsync(IsIsbnUniqueAsync)
            .WithMessage("Isbn already exists");
        
        // Release
        RuleFor(x => x.Release)
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now));
        
        // Id Category
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .NotNull()
            .MustAsync(CategoryExists)
            .WithMessage("Category does not exist");
        
        // Id Author
        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .NotNull()
            .MustAsync(AuthorExists)
            .WithMessage("Author does not exist");
    }

    private async Task<bool> IsTitleUniqueAsync(string title, CancellationToken cancellationToken)
    {
        Option<Book> book = await _bookStore
            .GetBookByTitleAsync(title, cancellationToken)
            .ConfigureAwait(false);
        
        return book.IsNone;
    }

    private async Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken cancellationToken)
    {
        Option<Book> book = await _bookStore
            .GetBookByIsbnAsync(isbn, cancellationToken)
            .ConfigureAwait(false);
        
        return book.IsNone;
    }

    private async Task<bool> CategoryExists(Guid categoryId, CancellationToken cancellationToken)
    {
        Option<Category> category = await _categoryStore
            .GetByIdAsync(categoryId, cancellationToken)
            .ConfigureAwait(false);
        
        return category.IsSome;
    }

    private async Task<bool> AuthorExists(Guid authorId, CancellationToken cancellationToken)
    {
        Option<Author> author = await _authorStore
            .GetByIdAsync(authorId, cancellationToken)
            .ConfigureAwait(false);
        
        return author.IsSome;
    }
}
