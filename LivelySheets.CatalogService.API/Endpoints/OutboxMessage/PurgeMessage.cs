
using LivelySheets.CatalogService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LivelySheets.CatalogService.API.Endpoints.OutboxMessage
{
    public class PurgeMessage : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("messages/purge-message/{messageId}",
                async ([FromRoute] Guid messageId,
                [FromServices] IMediator mediator) =>
                {
                    await mediator.Send(new PurgeMessageCommand { MessageId = messageId });
                    return Results.NoContent();
                }
            );
        }
    }
}
