using CardStorageService.Data;

namespace CardStorageService.Services.Impl
{
    public class CardRepository : ICardRepositoryService
    {
        private readonly CardStorageServiceDbContext context;
        private readonly ILogger<CardRepository> logger; // было <ClientRepository>

        public CardRepository(CardStorageServiceDbContext Context, ILogger<CardRepository> Logger)
        {
            context = Context;
            logger = Logger;
        }

        public int Create(Card data)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetByClientId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetByClientId(string id)
        {
            throw new NotImplementedException();
        }

        public Card GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Card data)
        {
            throw new NotImplementedException();
        }
    }
}
