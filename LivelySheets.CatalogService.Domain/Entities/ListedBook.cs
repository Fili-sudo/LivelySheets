using LivelySheets.CatalogService.Domain.Entities.Enums;

namespace LivelySheets.CatalogService.Domain.Entities;

public class ListedBook : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public ListedBookStatusEnum Status { get; set; }
}
