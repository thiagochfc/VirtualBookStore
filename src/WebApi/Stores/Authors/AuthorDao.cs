namespace VirtualBookstore.WebApi.Stores.Authors;

public record AuthorDao(Guid Id, string Name, string Email, string Description, DateTime Registered);
