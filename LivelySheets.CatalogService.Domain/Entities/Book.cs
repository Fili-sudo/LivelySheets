namespace LivelySheets.CatalogService.Domain.Entities;

public class Book : Entity
{
    public required string Title { get; set; }
    public List<ListedBook> ListedBooks { get; set; } = [];
}
