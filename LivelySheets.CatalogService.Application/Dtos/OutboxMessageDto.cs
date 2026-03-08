using LivelySheets.CatalogService.Domain.Entities.Messages;

namespace LivelySheets.CatalogService.Application.Dtos
{
    public class OutboxMessageDto
    {
        public string Body { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public Guid? InboxMessageId { get; set; }
        public int RetryCount { get; set; }


        public static implicit operator OutboxMessageDto(OutboxMessage outboxMessage) =>
            new ()
            {
                Body = outboxMessage.Body,
                SentOn = outboxMessage.SentOn,
                UpdatedOn = outboxMessage.UpdatedOn,
                InboxMessageId = outboxMessage.InboxMessageId,
                RetryCount = outboxMessage.RetryCount,
            };
    }
}
