using LivelySheets.CatalogService.Application.Commands;
using LivelySheets.CatalogService.Application.Interfaces;
using LivelySheets.CatalogService.Domain.Entities.Messages;
using MediatR;
using System.Text.Json;

namespace LivelySheets.CatalogService.Application.CommandHandlers;

public class FindBattleCommandHandler(IGenericRepository<OutboxMessage> outboxMessageRepository) : 
    IRequestHandler<FindBattleCommand, Guid>
{
    public async Task<Guid> Handle(FindBattleCommand request, CancellationToken cancellationToken)
    {
        var outboxMessage = new OutboxMessage
        {
            Body = JsonSerializer.Serialize(request),
            SentOn = DateTimeOffset.Now,
        };

        //save outbox message
        await outboxMessageRepository.AddAsync(outboxMessage, cancellationToken);

        //send request to MatchupService with outboxMessage content
        //receive created inbox message identifier from MatchupService
        var inboxMessageGuid = Guid.NewGuid();

        //send message to RabbitMQ topic exchange

        //update outboxMessage with received inbox message identifier
        outboxMessage.InboxMessageId = inboxMessageGuid;
        await outboxMessageRepository.SaveAsync(cancellationToken);

        return outboxMessage.Id;
    }
}
