namespace LivelySheets.CatalogService.Application.Interfaces;

public interface IRabbitMqFacade
{
    Task PublishMessageAsync(string routingKey, string message);
}
