namespace LivelySheets.CatalogService.API.Contracts.ServiceModels;

public class PostCreateInboxMessage
{
    public Guid OutboxMessageId { get; set; }
    public string Body { get; set; }
    public int RetryCount { get; set; }
}
