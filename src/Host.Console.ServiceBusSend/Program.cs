using Infrastructure.ServiceBus.Repositories;

var repository = new ServiceBusRepository("Endpoint=sb://servicebuslabs.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FcalhOZxNlgdTtsOuVNk/mpmhoGSWaPrr+ASbA92IvI=");

await repository.Send("default-queue", 5, "hello world");
Console.ReadLine();
