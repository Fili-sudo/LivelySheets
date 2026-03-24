using LivelySheets.CatalogService.Application.Commands;
using LivelySheets.CatalogService.Application.Dtos;
using LivelySheets.CatalogService.Application.Interfaces;
using LivelySheets.CatalogService.Domain.Entities.Messages;
using MediatR;
using System.Net.Http;
using System.Text.Json;

namespace LivelySheets.CatalogService.Application.CommandHandlers;

public class FindBattleCommandHandler(
    IGenericRepository<OutboxMessage> outboxMessageRepository,
    IMatchupServiceClient matchupServiceClient) : 
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
        var httpResponse = await matchupServiceClient.SendOutboxMessageAsync((OutboxMessageDto)outboxMessage);

        //if service call fails, revert the outbox entity creation
        if (!httpResponse.IsSuccessStatusCode)
        {
            await outboxMessageRepository.DeleteAsync(outboxMessage.Id, cancellationToken);
            throw new HttpRequestException(httpResponse.ReasonPhrase);
        }
        var data = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var inboxMessageGuid = JsonSerializer.Deserialize<Guid>(data);

        //update outboxMessage with received inbox message identifier
        outboxMessage.InboxMessageId = inboxMessageGuid;
        await outboxMessageRepository.SaveAsync(cancellationToken);

        //send message to RabbitMQ topic exchange

        return outboxMessage.Id;
    }
}
