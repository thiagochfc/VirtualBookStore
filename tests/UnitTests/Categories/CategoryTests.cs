using VirtualBookstore.WebApi.Categories;

namespace VirtualBookstore.UnitTests.Categories;

public class CategoryTests
{
    [Fact(DisplayName = "Should successfully create a category")]
    public void ShouldCreateCategorySuccessfully()
    {
        // Act
        var categoryResult = Category.Create("Action");
        Category category = categoryResult.ResultValue;
        
        // Assert
        Assert.True(categoryResult);
        Assert.NotEqual(Guid.Empty, category.Id);
    }

    [Fact(DisplayName = "Should not create a new category due to invalid parameters")]
    public void ShouldNotCreateCategoryDueToInvalidParameters()
    {
        // Act
        var categoryResult = Category.Create(string.Empty);
        
        // Assert
        Assert.False(categoryResult);
        Assert.Equal(CategoryErrors.NameNotProvidedError, categoryResult.ErrorValue);
    }
}
