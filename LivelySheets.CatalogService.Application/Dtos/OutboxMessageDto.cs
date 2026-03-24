using LivelySheets.CatalogService.Domain.Entities.Messages;

namespace LivelySheets.CatalogService.Application.Dtos
{
    public class OutboxMessageDto
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public Guid? InboxMessageId { get; set; }
        public int RetryCount { get; set; }


        public static implicit operator OutboxMessageDto(OutboxMessage outboxMessage) =>
            new ()
            {
                Id = outboxMessage.Id,
                Body = outboxMessage.Body,
                SentOn = outboxMessage.SentOn,
                UpdatedOn = outboxMessage.UpdatedOn,
                InboxMessageId = outboxMessage.InboxMessageId,
                RetryCount = outboxMessage.RetryCount,
            };

        public static explicit operator OutboxMessage(OutboxMessageDto dto) =>
            new()
            {
                Id = dto.Id,
                Body = dto.Body,
                SentOn = dto.SentOn,
                UpdatedOn = dto.UpdatedOn,
                InboxMessageId = dto.InboxMessageId,
                RetryCount= dto.RetryCount,
            };
    }
}
