using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPerformanceWebAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public string Name { get; set; } // e.g., "Microsoft"
        public string Symbol { get; set; } // e.g., "IBM"
        public int Quantity { get; set; }
        public double SharePriceAtPurchase { get; set; }
        public double CurrentSharePrice { get; set; }
        public DateTime TransactionTime { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to User
    }

    public enum TransactionType
    {
        Buy,
        Sell
    }
}
