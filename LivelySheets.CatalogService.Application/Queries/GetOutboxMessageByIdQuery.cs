using LivelySheets.CatalogService.Application.Dtos;
using LivelySheets.CatalogService.Domain.Entities.Messages;
using MediatR;

namespace LivelySheets.CatalogService.Application.Queries;

public class GetOutboxMessageByIdQuery : IRequest<OutboxMessageDto?>
{
    public Guid MessageId { get; set; }
}
