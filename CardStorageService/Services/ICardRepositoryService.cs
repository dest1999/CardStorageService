using CardStorageService.Data;

namespace CardStorageService.Services
{
    public interface ICardRepositoryService : IRepository<Card, int>
    {
        IList<Card> GetByClientId(int id);
    }
}
