using Azure.Messaging.ServiceBus;

namespace ServiceBus.Queue.Send;

public class Program
{
    private static async Task Main(string[] args)
    {
        string connectionString;
        string topic;
        string message;
        int numberOfMessages;

        if (args.Length == 0)
        {
            Console.WriteLine("Insert Event hub namespace connection string:");
            connectionString = Console.ReadLine()!;

            Console.WriteLine("Insert topic name:");
            topic = Console.ReadLine()!;

            Console.WriteLine("Insert message:");
            message = Console.ReadLine()!;

            Console.WriteLine("Insert number of messages you want to send. Default 1:");
            numberOfMessages = int.Parse(Console.ReadLine() ?? "1");
        }
        else
        {
            connectionString = args[0];
            topic = args[1];
            message = args[2];
            numberOfMessages = int.Parse(args[3]);
        }

        ServiceBusClient client;
        ServiceBusSender sender;

        // The Service Bus client types are safe to cache and use as a singleton for the lifetime
        // of the application, which is best practice when messages are being published or read
        // regularly.
        client = new ServiceBusClient(connectionString);
        sender = client.CreateSender(topic);

        // create a batch 
        using var messageBatch = await sender.CreateMessageBatchAsync();

        for (var i = 1; i <= numberOfMessages; i++)
            // try adding a message to the batch
            if (!messageBatch.TryAddMessage(new ServiceBusMessage(message)))
                throw new Exception($"The message {i} - {message} is too large to fit in the batch.");

        try
        {
            // Use the producer client to send the batch of messages to the Service Bus topic
            await sender.SendMessagesAsync(messageBatch);
            Console.WriteLine($"A batch of {numberOfMessages} messages has been published to the topic.");
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            await sender.DisposeAsync();
            await client.DisposeAsync();
        }

        Console.WriteLine("Press any key to end the application");
        Console.ReadKey();
    }
}