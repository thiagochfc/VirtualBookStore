namespace VirtualBookstore.UnitTests.Books;

public static class Constants
{
    public const string Title = "Title";
    public const string Resume = "Resume";
    public const string Summary = "Summary";
    public const decimal Price = 20.00m;
    public const uint NumberOfPages = 100;
    public const string Isbn = "Isbn";
    public static readonly DateOnly Release = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
    public static readonly Guid IdCategory = Guid.CreateVersion7();
    public static readonly Guid IdAuthor = Guid.CreateVersion7();
}
