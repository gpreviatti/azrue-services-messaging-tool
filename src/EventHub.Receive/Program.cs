using Azure.Messaging.EventHubs.Consumer;
using System.Diagnostics;

Console.WriteLine("Insert Event hub namespace connection string:");
var connectionString = Console.ReadLine();

Console.WriteLine("Insert Event hub name:");
var eventHubName = Console.ReadLine();

Console.WriteLine("Insert consumer group name. Default value: $Default:");
var consumerGroup = Console.ReadLine() ?? "$Default";

Console.WriteLine("Insert timeout time:");
var cancelAfter = Console.ReadLine() ?? "60";

var consumer = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName);

try
{
    using var cancellationSource = new CancellationTokenSource();
    cancellationSource.CancelAfter(TimeSpan.FromSeconds(int.Parse(cancelAfter!)));

    await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(cancellationSource.Token))
    {
        var readFromPartition = partitionEvent.Partition.PartitionId;
        var eventBodyBytes = partitionEvent.Data.EventBody.ToArray();

        Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {readFromPartition}");
    }
}
catch (TaskCanceledException)
{
    // This is expected if the cancellation token is
    // signaled.
}
finally
{
    await consumer.CloseAsync();
}