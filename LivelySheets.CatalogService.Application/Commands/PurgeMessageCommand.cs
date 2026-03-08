using MediatR;

namespace LivelySheets.CatalogService.Application.Commands;

public class PurgeMessageCommand : IRequest
{
    public Guid MessageId { get; set; }
}
