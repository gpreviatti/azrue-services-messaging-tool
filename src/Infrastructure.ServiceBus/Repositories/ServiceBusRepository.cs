using Azure.Messaging.ServiceBus;

namespace Infrastructure.ServiceBus.Repositories;
public class ServiceBusRepository
{
    private readonly ServiceBusClient _client;

    public ServiceBusRepository(string connectionString)
    {
        var clientOptions = new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };

        // the client that owns the connection and can be used to create senders and receivers
        _client = new ServiceBusClient(connectionString, clientOptions);
    }

    public async Task Send(string queue, int numberOfMessages, string message)
    {
        // create a batch 
        var sender = _client.CreateSender(queue);
        using var messageBatch = await sender.CreateMessageBatchAsync();

        try
        {
            for (var i = 1; i <= numberOfMessages; i++)
                messageBatch.TryAddMessage(new ServiceBusMessage(message));

            // Use the producer client to send the batch of messages to the Service Bus queue
            await sender.SendMessagesAsync(messageBatch);
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            await sender.DisposeAsync();
            await _client.DisposeAsync();
        }
    }

    public async Task Listen(string queue)
    {
        // create a processor that we can use to process the messages
        var processor = _client.CreateProcessor(queue, new ServiceBusProcessorOptions());

        try
        {
            // add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();

            // stop processing
            if (processor.AutoCompleteMessages)
                await processor.StopProcessingAsync();
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            await processor.DisposeAsync();
            await _client.DisposeAsync();
        }
    }

    /// <summary>
    /// handle received messages
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();

        await args.CompleteMessageAsync(args.Message);
    }

    /// <summary>
    /// handle any errors when receiving messages
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}
