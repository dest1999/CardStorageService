using CardStorageService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

        public string Create(Card data)
        {
            var client = context.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client == null)
                throw new Exception("Client not found.");
            context.Cards.Add(data);

            context.SaveChanges();

            return data.CardId.ToString();
        }

        public IList<Card> GetByClientId(string id)
        {
            List<Card> cards = context.Cards.Where(c => c.ClientId.ToString() == id).ToList();

            return cards;

        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }
        public IList<Card> GetAll()
        {
            throw new NotImplementedException();
        }
        public int Update(Card data)
        {
            throw new NotImplementedException();
        }
        public Card GetById(string id)
        {
            throw new NotImplementedException();
        }


    }
}
