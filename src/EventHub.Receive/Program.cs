using Azure.Messaging.EventHubs.Consumer;
using System.Diagnostics;

namespace EventHub.Receive;

public class Program
{
    private static async Task Main(string[] args)
    {
        string connectionString;
        string consumerGroup;
        int cancelAfter;

        if (args.Length == 0)
        {
            Console.WriteLine("Insert Event hub namespace connection string:");
            connectionString = Console.ReadLine()!;

            Console.WriteLine("Insert consumer group name. Default value: $Default:");
            consumerGroup = Console.ReadLine() ?? "$Default";

            Console.WriteLine("Insert timeout time:");
            cancelAfter = int.Parse(Console.ReadLine() ?? "60");
        } else
        {
            connectionString = args[0];
            consumerGroup = args[1];
            cancelAfter = int.Parse(args[2]);
        }

        var consumer = new EventHubConsumerClient(consumerGroup, connectionString);

        try
        {
            using var cancellationSource = new CancellationTokenSource();
            cancellationSource.CancelAfter(TimeSpan.FromSeconds(cancelAfter));

            await foreach (var partitionEvent in consumer.ReadEventsAsync(cancellationSource.Token))
            {
                var readFromPartition = partitionEvent.Partition.PartitionId;
                var eventBodyBytes = partitionEvent.Data.EventBody.ToArray();

                Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {readFromPartition}");
            }
        }
        catch (TaskCanceledException taskException)
        {
            Console.WriteLine(taskException.Message);
        }
        finally
        {
            await consumer.CloseAsync();
        }
    }
}