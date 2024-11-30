using Moonad;

namespace VirtualBookstore.WebApi.Categories;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    private Category(Guid id, string name) =>
        (Id, Name) = (id, name);

    public static Result<Category, ICategoryError> Create(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return CategoryErrors.NameNotProvidedError;
        }
        
        Category category = new(Guid.CreateVersion7(), name);
        return category;
    }
}
