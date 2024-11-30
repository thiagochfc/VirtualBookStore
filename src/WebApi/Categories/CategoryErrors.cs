namespace VirtualBookstore.WebApi.Categories;

public interface ICategoryError {}

public static class CategoryErrors
{
    public static readonly NameNotProvidedError NameNotProvidedError;
}

public readonly struct NameNotProvidedError : ICategoryError {}
