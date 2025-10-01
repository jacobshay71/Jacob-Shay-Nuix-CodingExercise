namespace InvestmentPerformanceWebAPI.DataTransferObjects
{
    public class UserDTO
    {
            public int Id { get; set; }
            public string Username { get; set; }

            public ICollection<TransactionDetailsDTO> Transactions { get; set; } = new List<TransactionDetailsDTO>();
    }
}
