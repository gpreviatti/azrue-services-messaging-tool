using Azure.Messaging.ServiceBus;

Console.WriteLine("Insert Service bus connection string:");
var connectionString = Console.ReadLine();

Console.WriteLine("Insert queue name:");
var queue = Console.ReadLine();

Console.WriteLine("Insert message");
var message = Console.ReadLine();

Console.WriteLine("Insert number of messages you want to send. Default 1:");
var numberOfMessages = int.Parse(Console.ReadLine() ?? "1");

ServiceBusClient client;
ServiceBusSender sender;

var clientOptions = new ServiceBusClientOptions()
{
    TransportType = ServiceBusTransportType.AmqpWebSockets
};
client = new ServiceBusClient(connectionString, clientOptions);
sender = client.CreateSender(queue);

// create a batch 
using var messageBatch = await sender.CreateMessageBatchAsync();

for (var i = 1; i <= numberOfMessages; i++)
{
    // try adding a message to the batch
    if (!messageBatch.TryAddMessage(new ServiceBusMessage(message)))
        throw new Exception($"The message {i} - {message} is too large to fit in the batch.");
}

try
{
    // Use the producer client to send the batch of messages to the Service Bus queue
    await sender.SendMessagesAsync(messageBatch);
    Console.WriteLine($"A batch of {numberOfMessages} messages has been published to the queue.");
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