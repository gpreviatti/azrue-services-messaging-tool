using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text;

namespace EventHub.Send;

public class Program
{
    private static async Task Main(string[] args)
    {
        string connectionString;
        string message;
        int numberOfEvents;

        if (args.Length == 0)
        {
            Console.WriteLine("Insert Event hub namespace connection string:");
            connectionString = Console.ReadLine()!;

            Console.WriteLine("Insert the message:");
            message = Console.ReadLine()!;

            Console.WriteLine("Insert number of messages you want to send. Default value: 1:");
            numberOfEvents = int.Parse(Console.ReadLine() ?? "1");
        }
        else
        {
            connectionString = args[0];
            message = args[1];
            numberOfEvents = int.Parse(args[2]);
        }

        // The Event Hubs client types are safe to cache and use as a singleton for the lifetime
        // of the application, which is best practice when events are being published or read regularly.
        var producerClient = new EventHubProducerClient(connectionString);

        // Create a batch of events 
        using var eventBatch = await producerClient.CreateBatchAsync();

        for (var i = 1; i <= numberOfEvents; i++)
            if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(message!))))
                // if it is too large for the batch
                throw new Exception($"Event {i} is too large for the batch and cannot be sent.");

        try
        {
            // Use the producer client to send the batch of events to the event hub
            await producerClient.SendAsync(eventBatch);
            Console.WriteLine($"A batch of {numberOfEvents} events has been published.");
        } catch(Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
        finally
        {
            await producerClient.DisposeAsync();
        }
    }
}