using LivelySheets.CatalogService.Application.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace LivelySheets.CatalogService.Infrastructure.Messaging;

public class RabbitMqFacade : IRabbitMqFacade, IAsyncDisposable
{
    private readonly ConnectionFactory _factory;

    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMqFacade()
    {
        _factory = new ConnectionFactory { HostName = "localhost" };
    }

    public async Task PublishMessageAsync(string routingKey, string message)
    {
        await SetupChannel();

        await _channel!.ExchangeDeclareAsync(exchange: "topic_logs", type: ExchangeType.Topic);
        var body = Encoding.UTF8.GetBytes(message);

        await _channel!.BasicPublishAsync(exchange: "topic_logs", routingKey: routingKey, body: body);
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection is not null && _connection.IsOpen)
            await _connection.CloseAsync();

        _connection = null;
        _channel = null;
    }

    private async ValueTask SetupChannel()
    {
        await SetupConnection();
        if (_channel is null || !_channel.IsOpen)
            _channel = await _connection!.CreateChannelAsync();
    }

    private async ValueTask SetupConnection()
    {
        if (_connection is null || !_connection.IsOpen)
            _connection = await _factory.CreateConnectionAsync();
    }
}
