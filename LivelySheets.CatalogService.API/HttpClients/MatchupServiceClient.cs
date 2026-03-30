using LivelySheets.CatalogService.API.Contracts.ServiceModels;
using LivelySheets.CatalogService.Application.Dtos;
using LivelySheets.CatalogService.Application.Interfaces;
using System.Text;
using System.Text.Json;

namespace LivelySheets.CatalogService.API.HttpClients;

public class MatchupServiceClient(HttpClient httpClient) : IMatchupServiceClient
{
    private readonly string CreateInboxMessageEndpoint = "messages/create-message";

    public async Task<HttpResponseMessage> SendOutboxMessageAsync(OutboxMessageDto outboxMessage)
    {
        var inboxMessage = new PostCreateInboxMessage
        {
            OutboxMessageId = outboxMessage.Id,
            Body = outboxMessage.Body,
            RetryCount = outboxMessage.RetryCount,
        };
        var requestBody = JsonSerializer.Serialize(inboxMessage);
        var request = new StringContent(requestBody, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(CreateInboxMessageEndpoint, request);
        return response;
    }
}
