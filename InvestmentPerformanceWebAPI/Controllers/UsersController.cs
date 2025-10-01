using InvestmentPerformanceWebAPI.Database;
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
            var user = _context.Users.Include("Transactions").FirstOrDefault(u => u.Id == id);

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
