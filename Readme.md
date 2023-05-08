# Azure messaging tools samples

Basic project with implementation sample projects provides by Microsoft documentation of common messaging tools of azure

## Service Bus

### Queue

[Link reference](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues?tabs=connection-string)

#### How to use

To send messages

```
servicebus-queue-send-win-x64.exe "<connection string>" "<queue-name>" "<message>" "<times to send message>"
```

To listen messages

```
servicebus-queue-receive-win-x64.exe "<connection string>" "<queue name>"
```

### Topic

[Link reference](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-how-to-use-topics-subscriptions?tabs=connection-string)

To send messages

```
servicebus-topic-send-win-x64.exe "<connection string>" "<topic name>" "<message>" "<times to send message>"

```

To listen messages

```
servicebus-topic-receive-win-x64.exe "<connection string>" "<topic name>"
```

## Event Hub

[Link reference](https://learn.microsoft.com/en-us/azure/event-hubs/event-hubs-dotnet-standard-getstarted-send?tabs=connection-string%2Croles-azure-portal)

To send messages

```
eventhub-send-win-x64.exe "<connection string>" "<message>" "<times to send message>"

```

To listen messages

```
eventhub-receive-win-x64.exe "<connection string>" "<consumer group>" "<timout>"

```