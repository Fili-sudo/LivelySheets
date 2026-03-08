using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivelySheets.CatalogService.Domain.Entities.Messages;

public class OutboxMessage : Entity
{
    public string Body { get; set; }
    public DateTimeOffset SentOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public Guid? InboxMessageId { get; set; }
    public int RetryCount { get; set; }
}
