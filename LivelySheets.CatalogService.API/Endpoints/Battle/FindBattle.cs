using LivelySheets.CatalogService.API.Contracts.Requests;
using LivelySheets.CatalogService.API.Endpoints.OutboxMessage;
using LivelySheets.CatalogService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LivelySheets.CatalogService.API.Endpoints.Battle
{
    public class FindBattle : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("battles/find-battle",
                async (HttpContext context,
                LinkGenerator linkGenerator,
                [FromBody] PostFindBattleDto body,
                [FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send((FindBattleCommand)body);
                    var messageLink = linkGenerator.GetUriByName(
                        context, GetMessage.GetMessageEndpoint,
                        new { messageId = result }
                    );

                    return Results.Created(messageLink, body);
                }
            );
        }
    }
}
