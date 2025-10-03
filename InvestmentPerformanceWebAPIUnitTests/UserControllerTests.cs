using InvestmentPerformanceWebAPI.Controllers;
using InvestmentPerformanceWebAPI.Database;
using InvestmentPerformanceWebAPI.DataTransferObjects;
using InvestmentPerformanceWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvestmentPerformanceWebAPIUnitTests
{
    [TestClass]
    public class UserControllerTests
    {
        private ApplicationDbContext _context = null!;
        private UserController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new UserController(_context);
            SeedTestData();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        private void SeedTestData()
        {
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                Email = "test@example.com",
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Id = 1,
                        Name = "Apple Inc.",
                        Symbol = "AAPL",
                        Quantity = 10,
                        SharePriceAtPurchase = 150.00,
                        CurrentSharePrice = 175.00,
                        TransactionTime = DateTime.UtcNow.AddDays(-30),
                        UserId = 1,
                        Type = TransactionType.Buy
                    },
                    new Transaction
                    {
                        Id = 2,
                        Name = "Microsoft Corp.",
                        Symbol = "MSFT",
                        Quantity = 5,
                        SharePriceAtPurchase = 300.00,
                        CurrentSharePrice = 320.00,
                        TransactionTime = DateTime.UtcNow.AddDays(-15),
                        UserId = 1,
                        Type = TransactionType.Buy
                    }
                }
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        [TestMethod]
        public void GetUser_WithValidId_ReturnsOkResultWithUserDTO()
        {
            int userId = 1;
            var result = _controller.GetUser(userId);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            
            Assert.IsInstanceOfType(okResult.Value, typeof(IQueryable<UserDTO>));
            var userQuery = (IQueryable<UserDTO>)okResult.Value!;
            var userDto = userQuery.FirstOrDefault();
            
            Assert.IsNotNull(userDto);
            Assert.AreEqual(userId, userDto.Id);
            Assert.AreEqual("testuser", userDto.Username);
            Assert.AreEqual(2, userDto.Transactions.Count);
        }

        [TestMethod]
        public void GetUser_WithValidId_ReturnsCorrectTransactionDetails()
        {
            int userId = 1;
            var result = _controller.GetUser(userId);

            var okResult = (OkObjectResult)result;
            var userQuery = (IQueryable<UserDTO>)okResult.Value!;
            var userDto = userQuery.FirstOrDefault();
            
            Assert.IsNotNull(userDto);
            var appleTransaction = userDto.Transactions.FirstOrDefault(t => t.Name == "Apple Inc.");
            Assert.IsNotNull(appleTransaction);
            Assert.AreEqual(1, appleTransaction.Id);
            Assert.AreEqual(10, appleTransaction.Shares);
            Assert.AreEqual(150.00, appleTransaction.CostBasisPerShare);
            Assert.AreEqual(175.00, appleTransaction.CurrentPrice);
            Assert.AreEqual(1750.00, appleTransaction.CurrentValue);
            Assert.AreEqual(250.00, appleTransaction.TotalGain);

            var microsoftTransaction = userDto.Transactions.FirstOrDefault(t => t.Name == "Microsoft Corp.");
            Assert.IsNotNull(microsoftTransaction);
            Assert.AreEqual(2, microsoftTransaction.Id);
            Assert.AreEqual(5, microsoftTransaction.Shares);
            Assert.AreEqual(300.00, microsoftTransaction.CostBasisPerShare);
            Assert.AreEqual(320.00, microsoftTransaction.CurrentPrice);
            Assert.AreEqual(1600.00, microsoftTransaction.CurrentValue);
            Assert.AreEqual(100.00, microsoftTransaction.TotalGain);
        }

        [TestMethod]
        public void GetUser_WithInvalidId_ReturnsOkWithEmptyQueryable()
        {
            int invalidUserId = 999;
            var result = _controller.GetUser(invalidUserId);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            var userQuery = (IQueryable<UserDTO>)okResult.Value!;
            var userDto = userQuery.FirstOrDefault();
            
            Assert.IsNull(userDto);
        }

        [TestMethod]
        public void GetUser_NullContext_ThrowsNullReferenceException()
        {
            var controller = new UserController(null);

            Assert.ThrowsException<NullReferenceException>(() => controller.GetUser(1));
        }

        [TestMethod]
        public void GetUser_VerifyTransactionCalculations_AreCorrect()
        {
            var testUser = new User
            {
                Id = 3,
                Username = "calculationtest",
                Email = "calc@test.com",
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Id = 10,
                        Name = "Test Stock",
                        Symbol = "TEST",
                        Quantity = 100,
                        SharePriceAtPurchase = 50.0,
                        CurrentSharePrice = 60.0,
                        TransactionTime = DateTime.UtcNow,
                        UserId = 3,
                        Type = TransactionType.Buy
                    }
                }
            };

            _context.Users.Add(testUser);
            _context.SaveChanges();

            var result = _controller.GetUser(3);

            var okResult = (OkObjectResult)result;
            var userQuery = (IQueryable<UserDTO>)okResult.Value!;
            var userDto = userQuery.FirstOrDefault();
            Assert.IsNotNull(userDto);
            var transaction = userDto.Transactions.First();

            Assert.AreEqual(6000.0, transaction.CurrentValue);
            Assert.AreEqual(1000.0, transaction.TotalGain);
            Assert.AreEqual(50.0, transaction.CostBasisPerShare);
            Assert.AreEqual(60.0, transaction.CurrentPrice);
            Assert.AreEqual(100, transaction.Shares);
        }

        [TestMethod]
        public void GetUser_LossTransaction_CalculatesNegativeGain()
        {
            var userWithLoss = new User
            {
                Id = 4,
                Username = "lossuser",
                Email = "loss@example.com",
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Id = 20,
                        Name = "Loss Stock",
                        Symbol = "LOSS",
                        Quantity = 50,
                        SharePriceAtPurchase = 100.0,
                        CurrentSharePrice = 80.0,
                        TransactionTime = DateTime.UtcNow,
                        UserId = 4,
                        Type = TransactionType.Buy
                    }
                }
            };

            _context.Users.Add(userWithLoss);
            _context.SaveChanges();

            var result = _controller.GetUser(4);

            var okResult = (OkObjectResult)result;
            var userQuery = (IQueryable<UserDTO>)okResult.Value!;
            var userDto = userQuery.FirstOrDefault();
            Assert.IsNotNull(userDto);
            var transaction = userDto.Transactions.First();

            Assert.AreEqual(4000.0, transaction.CurrentValue);
            Assert.AreEqual(-1000.0, transaction.TotalGain);
        }
    }
}