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

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
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
            });

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
