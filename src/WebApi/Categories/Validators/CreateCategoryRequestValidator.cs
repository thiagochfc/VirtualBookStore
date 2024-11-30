using FluentValidation;

using Moonad;

using VirtualBookstore.WebApi.Categories.Models;

namespace VirtualBookstore.WebApi.Categories.Validators;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    private readonly ICategoryStore _categoryStore;
    
    public CreateCategoryRequestValidator(ICategoryStore categoryStore)
    {
        _categoryStore = categoryStore;
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .MustAsync(IsNameUnique)
            .WithMessage("Name already exists");
    }

    private async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken)
    {
        Option<Category> categoryOption = await _categoryStore
            .GetByNameAsync(name, cancellationToken)
            .ConfigureAwait(false);

        return categoryOption.IsNone;
    }
}
