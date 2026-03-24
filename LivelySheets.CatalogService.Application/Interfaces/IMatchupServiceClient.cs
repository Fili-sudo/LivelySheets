using LivelySheets.CatalogService.Application.Dtos;

namespace LivelySheets.CatalogService.Application.Interfaces;

public interface IMatchupServiceClient
{
    Task<HttpResponseMessage> SendOutboxMessageAsync(OutboxMessageDto outboxMessage);
}
