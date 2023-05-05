using Infrastructure.ServiceBus.Repositories;

var repository = new ServiceBusRepository("Endpoint=sb://servicebuslabs.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FcalhOZxNlgdTtsOuVNk/mpmhoGSWaPrr+ASbA92IvI=");

await repository.Listen("default-queue");

Console.WriteLine("");
Console.ReadLine();