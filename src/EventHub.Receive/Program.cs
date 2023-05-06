using Azure.Messaging.EventHubs.Consumer;
using System.Diagnostics;

var connectionString = "";
var eventHubName = "";

var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

var consumer = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName);

try
{
    using var cancellationSource = new CancellationTokenSource();
    cancellationSource.CancelAfter(TimeSpan.FromSeconds(120));

    var eventsRead = 0;
    var maximumEvents = 3;

    await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(cancellationSource.Token))
    {
        var readFromPartition = partitionEvent.Partition.PartitionId;
        var eventBodyBytes = partitionEvent.Data.EventBody.ToArray();

        Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {readFromPartition}");
        eventsRead++;

        if (eventsRead >= maximumEvents)
            break;
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