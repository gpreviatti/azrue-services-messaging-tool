using Azure.Messaging.ServiceBus;

Console.WriteLine("Insert Service bus connection string");
var connectionString = Console.ReadLine();

Console.WriteLine("Insert topic name:");
var topic = Console.ReadLine();

ServiceBusClient client;
ServiceBusProcessor processor;

// handle received messages
async Task MessageHandler(ProcessMessageEventArgs args)
{
    var body = args.Message.Body.ToString();
    Console.WriteLine($"Received: {body} from subscription.");

    // complete the message. messages is deleted from the subscription. 
    await args.CompleteMessageAsync(args.Message);
}

// handle any errors when receiving messages
Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}

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