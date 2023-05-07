using Azure.Messaging.ServiceBus;

namespace ServiceBus.Queue.Receive;

public class Program
{
    private static async Task Main(string[] args)
    {
        string connectionString;
        string queue;

        if (args.Length == 0)
        {
            Console.WriteLine("Insert Service bus connection string:");
            connectionString = Console.ReadLine()!;

            Console.WriteLine("Insert queue name");
            queue = Console.ReadLine()!;
        }
        else
        {
            connectionString = args[0];
            queue = args[1];
        }


        ServiceBusClient client;
        ServiceBusProcessor processor;

        var clientOptions = new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        client = new ServiceBusClient(connectionString, clientOptions);

        // create a processor that we can use to process the messages
        processor = client.CreateProcessor(queue, new ServiceBusProcessorOptions());

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
        Console.WriteLine($"Received: {body}");

        // complete the message. message is deleted from the queue. 
        await args.CompleteMessageAsync(args.Message);
    }

    // handle any errors when receiving messages
    public static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}