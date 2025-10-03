using InvestmentPerformanceWebAPI.Database;
using InvestmentPerformanceWebAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceWebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for handling transaction-related API endpoints.
    /// Provides access to transaction data including basic transaction information and detailed financial calculations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the TransactionsController.
        /// </summary>
        /// <param name="context">The Entity Framework database context used to access transaction data.</param>
        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all transactions associated with a specific user.
        /// Returns basic transaction information (ID and Name) for all transactions belonging to the specified user.
        /// </summary>
        /// <param name="id">The unique identifier of the user whose transactions should be retrieved.</param>
        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetTransactionsForUser(int id)
        {
            // Query the database for all transactions belonging to the specified user
            // Project the results to TransactionDTO to return only basic information
            var transactions = _context.Transactions.Where(t => t.UserId == id).Select( t => new TransactionDTO()
            {
                Id = t.Id,
                Name = t.Name
            });

            return Ok(transactions);
        }

        /// <summary>
        /// Retrieves basic information for a specific transaction by its ID.
        /// Returns only the transaction ID and company name.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to retrieve.</param>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTransaction(int id)
        {
            // Query for a specific transaction by ID and project to basic DTO
            // Note: Returns IQueryable which may contain 0 or 1 results
            var transactions = _context.Transactions.Where(t => t.Id == id).Select(t => new TransactionDTO()
            {
                Id = t.Id,
                Name = t.Name,
            });

            return Ok(transactions);
        }

        /// <summary>
        /// Retrieves comprehensive transaction details including financial calculations for a specific transaction.
        /// Returns detailed information including current market value, total gain/loss, and investment performance metrics.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to retrieve detailed information for.</param>
        [HttpGet]
        [Route("transaction-details/{id}")]
        public IActionResult GetTransactionDetails(int id)
        {
            // Query for specific transaction and project to detailed DTO with financial calculations
            var transactions = _context.Transactions.Where(t => t.Id == id).Select(t => new TransactionDetailsDTO()
            {
                Id = t.Id,
                Name = t.Name,
                Shares = t.Quantity, // Map Quantity to Shares for DTO
                TransactionTime = t.TransactionTime,
                CostBasisPerShare = t.SharePriceAtPurchase, // Original purchase price per share
                CurrentValue = t.CurrentSharePrice * t.Quantity, // Total current market value
                CurrentPrice = t.CurrentSharePrice, // Current market price per share
                // Calculate total gain/loss: (Current Value) - (Original Investment)
                TotalGain = (t.CurrentSharePrice * t.Quantity) - (t.SharePriceAtPurchase * t.Quantity)
            });

            return Ok(transactions);
        }
    }
}
