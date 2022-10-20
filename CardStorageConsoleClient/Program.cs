using Grpc.Net.Client;
using static CardStorageServiceProtos.CardService;
using static CardStorageServiceProtos.ClientService;

namespace CardStorageConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.Http2EnencryptedSupport", true);

            //CardServiceClient
            using var chan = GrpcChannel.ForAddress("http://localhost:5001");

            var clientService = new ClientServiceClient(chan);

            var createClientResponse = clientService.Create(new CardStorageServiceProtos.CreateClientRequest
            {
                FirstName = "User1",
                Surname = "Surn1",
                Patronymic = "Patr1"
            });

            Console.WriteLine($"Created client id {createClientResponse.ClientId}");

        }
    }
}