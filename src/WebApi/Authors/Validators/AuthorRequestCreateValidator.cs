using FluentValidation;

using Moonad;

using VirtualBookstore.WebApi.Authors.Models;

namespace VirtualBookstore.WebApi.Authors.Validators;

public class AuthorRequestCreateValidator : AbstractValidator<AuthorRequestCreate>
{
    private readonly IAuthorStore _authorStore;
    
    public AuthorRequestCreateValidator(IAuthorStore authorStore)
    {
        _authorStore = authorStore;
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(50)
            .MustAsync(IsEmailUnique)
            .WithMessage("Email already exists");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(Author.DescriptionMaxLength);
    }

    private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
    {
        Option<Author> author = await _authorStore
            .GetByEmailAsync(email, cancellationToken)
            .ConfigureAwait(false);

        return author.IsNone;
    }
}
