using Azure.Messaging.ServiceBus;

namespace ServiceBus.Topic.Receive;
public class Program
{
    private static async Task Main(string[] args)
    {
        string connectionString;
        string topic;

        if (args.Length == 0)
        {
            Console.WriteLine("Insert Event hub namespace connection string:");
            connectionString = Console.ReadLine()!;

            Console.WriteLine("Insert topic name:");
            topic = Console.ReadLine()!;
        }
        else
        {
            connectionString = args[0];
            topic = args[1];
        }

        ServiceBusClient client;
        ServiceBusProcessor processor;

        client = new ServiceBusClient(connectionString);
        processor = client.CreateProcessor(topic, new ServiceBusProcessorOptions());

        try
        {
            // add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();

            Console.WriteLine("Wait for a minute and then press any key to end the processing");
            Console.ReadKey();

            // stop processing 
            Console.WriteLine("\nStopping the receiver...");
            await processor.StopProcessingAsync();
            Console.WriteLine("Stopped receiving messages");
        }
        finally
        {
            await processor.DisposeAsync();
            await client.DisposeAsync();
        }
    }

    // handle received messages
    public static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        Console.WriteLine($"Received: {body} from subscription.");

        // complete the message. messages is deleted from the subscription. 
        await args.CompleteMessageAsync(args.Message);
    }

    // handle any errors when receiving messages
    public static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}