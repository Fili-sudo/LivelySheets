namespace LivelySheets.CatalogService.Domain.Entities;

public class User : Entity
{
    public required string Username { get; set; }
    public List<ListedBook> ListedBooks { get; set; } = [];

}
