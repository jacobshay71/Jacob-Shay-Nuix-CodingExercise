using InvestmentPerformanceWebAPI.Database;
using InvestmentPerformanceWebAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetTransactionsForUser(int id)
        {
            var transactions = _context.Transactions.Where(t => t.UserId == id).Select( t => new TransactionDTO()
            {
                Id = t.Id,
                Name = t.Name
            });

            return Ok(transactions);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTransaction(int id)
        {
            var transactions = _context.Transactions.Where(t => t.Id == id).Select(t => new TransactionDTO()
            {
                Id = t.Id,
                Name = t.Name,
            });

            return Ok(transactions);
        }

        [HttpGet]
        [Route("transaction-deatils/{id}")]
        public IActionResult GetTransactionDetails(int id)
        {
            var transactions = _context.Transactions.Where(t => t.Id == id).Select(t => new TransactionDetailsDTO()
            {
                Id = t.Id,
                Name = t.Name,
                Shares = t.Quantity,
                TransactionTime = t.TransactionTime,
                CostBasisPerShare = t.SharePriceAtPurchase,
                CurrentValue = t.CurrentSharePrice * t.Quantity,
                CurrentPrice = t.CurrentSharePrice,
                TotalGain = (t.CurrentSharePrice * t.Quantity) - (t.SharePriceAtPurchase * t.Quantity)
            });

            return Ok(transactions);
        }
    }
}
