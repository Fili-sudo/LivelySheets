using MediatR;

namespace LivelySheets.CatalogService.Application.Commands;

public class FindBattleCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}
