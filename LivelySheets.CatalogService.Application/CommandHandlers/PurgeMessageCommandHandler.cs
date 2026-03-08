using LivelySheets.CatalogService.Application.Commands;
using LivelySheets.CatalogService.Application.Interfaces;
using LivelySheets.CatalogService.Domain.Entities.Messages;
using MediatR;

namespace LivelySheets.CatalogService.Application.CommandHandlers
{
    public class PurgeMessageCommandHandler(IGenericRepository<OutboxMessage> outboxMessageRepository) : 
        IRequestHandler<PurgeMessageCommand>
    {
        public async Task Handle(PurgeMessageCommand request, CancellationToken cancellationToken)
        {
            //send delete request to MatchupService to delete inbox message entry
            //if successfull, delete outbox message as well

            await outboxMessageRepository.DeleteAsync(request.MessageId, cancellationToken);
        }
    }
}
