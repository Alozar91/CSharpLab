using Grpc.Net.Client;

using static GrpcDataServiceClient.PeopleService;

namespace GrpcDataServiceClient
{
    internal class Program
    {
        async static Task Main(string[] args)
        {

            using var channel = GrpcChannel.ForAddress("http://localhost:5041");

            var client = new PeopleServiceClient(channel);
            client.CreatePerson(new CreatePersonRequest() { Name = "Vasya", Age = 69 });
            var reply = await client.ListPeopleAsync(new Google.Protobuf.WellKnownTypes.Empty());
            foreach (var p in reply.People)
                Console.WriteLine($"{p.Id}.{p.Name} - {p.Age}");

            Console.ReadLine();
        }
    }
}