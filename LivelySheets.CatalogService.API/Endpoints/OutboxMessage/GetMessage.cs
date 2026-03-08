using LivelySheets.CatalogService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LivelySheets.CatalogService.API.Endpoints.OutboxMessage
{
    public class GetMessage : IEndpoint
    {
        internal static readonly string GetMessageEndpoint = nameof(GetMessageEndpoint);
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("messages/{messageId}",
                async ([FromRoute] Guid messageId,
                [FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetOutboxMessageByIdQuery { MessageId = messageId });
                    return Results.Ok(result);
                }
            ).WithName(GetMessageEndpoint);
        }
    }
}
