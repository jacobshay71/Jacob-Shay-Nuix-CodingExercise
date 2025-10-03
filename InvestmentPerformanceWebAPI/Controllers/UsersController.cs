using InvestmentPerformanceWebAPI.Database;
using InvestmentPerformanceWebAPI.DataTransferObjects;
using InvestmentPerformanceWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InvestmentPerformanceWebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the UserController.
        /// </summary>
        /// <param name="context">Database context for accessing user and transaction data.</param>
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all users with basic information (ID and username only).
        /// </summary>
        [HttpGet]
        public IActionResult GetUsers()
        {
            // Query all users and project to basic UserDTO
            var users = _context.Users.Select(u => new UserDTO()
            {
                Id = u.Id,
                Username = u.Username
            });
            
            // Note: This null check is unnecessary as LINQ Select never returns null
            if (users == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(users);
            }
        }

        /// <summary>
        /// Retrieves detailed user information including complete investment portfolio with financial calculations.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            // Query specific user with transactions, calculate financial metrics in projection
            var user = _context.Users.Include("Transactions").Where(u => u.Id == id).Select(u => new UserDTO()
            {
                Id = u.Id,
                Username = u.Username,
                Transactions = u.Transactions.Select(t => new TransactionDetailsDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Shares = t.Quantity,
                    TransactionTime = t.TransactionTime,
                    CostBasisPerShare = t.SharePriceAtPurchase,
                    CurrentValue = t.CurrentSharePrice * t.Quantity,
                    CurrentPrice = t.CurrentSharePrice,
                    TotalGain = (t.CurrentSharePrice * t.Quantity) - (t.SharePriceAtPurchase * t.Quantity)
                }).ToList()
            }).FirstOrDefault();

            // return bad request if user not found
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
