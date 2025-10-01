namespace InvestmentPerformanceWebAPI.DataTransferObjects
{
    public class TransactionDetailsDTO : TransactionDTO
    {
        public DateTime TransactionTime { get; set; }
        public int Shares { get; set; }
        public string Term
        {
            get
            {
                return TransactionTime >= DateTime.UtcNow.AddYears(-1) ? "Short-Term" : "Long-Term";
            }
        }
        public double CostBasisPerShare { get; set; }

        public double CurrentValue {get; set;}

        public double CurrentPrice { get; set; }

        public double TotalGain { get; set; }
    }
}
