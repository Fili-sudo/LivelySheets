using LivelySheets.CatalogService.Application.Commands;
using LivelySheets.CatalogService.Application.Interfaces;
using LivelySheets.CatalogService.Domain.Entities.Messages;
using MediatR;

namespace LivelySheets.CatalogService.Application.CommandHandlers
{
    public class PurgeMessageCommandHandler(
        IGenericRepository<OutboxMessage> outboxMessageRepository,
        IMatchupServiceClient matchupServiceClient) :
        IRequestHandler<PurgeMessageCommand>
    {
        public async Task Handle(PurgeMessageCommand request, CancellationToken cancellationToken)
        {
            var outboxMessage = await outboxMessageRepository.GetByIdAsync(request.MessageId, cancellationToken);
            if (outboxMessage != null && outboxMessage!.InboxMessageId.HasValue)
            {
                var httpResponse = await matchupServiceClient.DeleteInboxMessageAsync(outboxMessage.InboxMessageId.Value);
                httpResponse.EnsureSuccessStatusCode();
            }

            await outboxMessageRepository.DeleteAsync(request.MessageId, cancellationToken);
        }
    }
}
