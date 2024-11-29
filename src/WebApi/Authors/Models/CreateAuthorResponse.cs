namespace VirtualBookstore.WebApi.Authors.Models;

public record CreateAuthorResponse(string Id, string Name, string Email, string Description, string Registered)
{
    internal static CreateAuthorResponse From(Author author) =>
        new(author.Id.ToString(),
            author.Name,
            author.Email,
            author.Description,
            author.Registered.ToString("O"));
};
