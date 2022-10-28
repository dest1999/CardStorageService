using CardStorageServiceProtos;
using Grpc.Core;
using static CardStorageServiceProtos.ClientService;

namespace CardStorageService.Services.Impl
{
    public class ClientService : ClientServiceBase
    {
        private readonly IClientRepositoryService clientRepositoryService;
        public ClientService(IClientRepositoryService ClientRepositoryService)
        {
            clientRepositoryService = ClientRepositoryService;
        }

        public override Task<CreateClientResponse> Create(CreateClientRequest request, ServerCallContext context)
        {
            var clientId = clientRepositoryService.Create(new Data.Client
            {
                FirstName = request.FirstName,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
            });

            var response = new CreateClientResponse
            {
                ClientId = clientId 
            };

            return Task.FromResult(response);
        }
    }
}
