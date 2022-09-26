namespace CardStorageService.Models.Requests
{
    public class GetCardsResponse : IOperationResult
    {
        public IList<CardDTO> Cards { get; set; }
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
