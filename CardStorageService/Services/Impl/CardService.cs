using CardStorageServiceProtos;
using Grpc.Core;
using static CardStorageServiceProtos.CardService;

namespace CardStorageService.Services.Impl
{
    public class CardService : CardServiceBase
    {
        private readonly ICardRepositoryService cardRepositoryService;
        public CardService(ICardRepositoryService CardRepositoryService)
        {
            cardRepositoryService = CardRepositoryService;
        }

        public override Task<GetByClientIdResponse> GetByClientId(GetByClientIdRequest request, ServerCallContext context)
        {
            var response = new GetByClientIdResponse();

            response.Cards.AddRange(cardRepositoryService.GetByClientId(request.ClientId.ToString())
                .Select(card => new Card
                {
                    CardNo = card.CardNo,
                    CVV2 = card.CVV2,
                    ExpDate = card.ExpDate.ToString("MM/yy"),
                    Name = card.Name,
                }).ToList());

            return Task.FromResult(response);
        }
    }
}
