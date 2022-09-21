using CardStorageService.Data;

namespace CardStorageService.Services.Impl
{
    public class ClientRepository : IClientRepositoryService
    {
        private readonly CardStorageServiceDbContext context;
        private readonly ILogger<ClientRepository> logger;

        public ClientRepository(CardStorageServiceDbContext Context, ILogger<ClientRepository> Logger)
        {
            context = Context;
            logger = Logger;
        }

        public int Create(Client data)
        {
            context.Clients.Add(data);
            context.SaveChanges();
            return data.ClientId;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Client data)
        {
            throw new NotImplementedException();
        }
    }
}
