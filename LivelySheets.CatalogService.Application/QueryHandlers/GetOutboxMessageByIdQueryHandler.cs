using LivelySheets.CatalogService.Application.Dtos;
using LivelySheets.CatalogService.Application.Interfaces;
using LivelySheets.CatalogService.Application.Queries;
using LivelySheets.CatalogService.Domain.Entities.Messages;
using MediatR;

namespace LivelySheets.CatalogService.Application.QueryHandlers;

public class GetOutboxMessageByIdQueryHandler(IGenericRepository<OutboxMessage> outboxMessageRepository) :
    IRequestHandler<GetOutboxMessageByIdQuery, OutboxMessageDto?>
{
    public async Task<OutboxMessageDto?> Handle(GetOutboxMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await outboxMessageRepository.GetByIdAsync(request.MessageId, cancellationToken);
        if (result is null)
            return null;

        return result;
    }
}
