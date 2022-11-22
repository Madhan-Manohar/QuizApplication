using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizAPiService.Entities;
using QuizAPiService.Service;
using QuizAPiService.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace QuizApiTesting
{

    [TestClass]
    public class QuizdetailTesting
    {
        private readonly QuizContext _dbContext;
        private readonly IQuizdetailService _quizdetailService;
        private readonly ILogger<QuizdetailService> _logger;
        public QuizdetailTesting()
        {
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _quizdetailService = new QuizdetailService(_dbContext, _logger);
        }
        [TestInitialize]
        public void TestInitialize()
        {
            Console.WriteLine("Inside TestInitialize");
        }
        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext.Dispose();
            Console.WriteLine("Inside TestCleanup");
        }
        [TestMethod]
        public void GetQuizDetailTest()
        {
            var items = new List<Quizdetail>
            {
                new Quizdetail { QuizId = 1, CategoryId = 1, LevelId = 1, EmployeeId = 1, CompanyId = 1, ExpiresOn=DateTime.Now, Status="Active", TotalScore=100, SecureScore=0, IsActive=1, CreatedBy=1, CreatedOn=DateTime.Now, ModifiedBy=1,ModifiedOn=DateTime.Now },
                new Quizdetail { QuizId = 2, CategoryId = 2, LevelId = 2, EmployeeId = 3, CompanyId = 2, ExpiresOn=DateTime.Now, Status="Closed", TotalScore=100, SecureScore=55, IsActive=1, CreatedBy=2, CreatedOn=DateTime.Now, ModifiedBy=1,ModifiedOn=DateTime.Now }

            };

            _dbContext.Quizdetails.AddRange(items);
            _dbContext.SaveChanges();

            var getone = _dbContext.Quizdetails.Where(x => x.QuizId == 1).First();
            var gettwo = _dbContext.Quizdetails.Where(x => x.QuizId == 2).First();
            
            Assert.IsTrue(getone.LevelId == 1);
            Assert.IsTrue(gettwo.LevelId != 1);
            Assert.IsTrue(gettwo.EmployeeId == 3);
        }
        [TestMethod]
        public void GetQuizdetailByIdTest()
        {
            _dbContext.Quizdetails.Add(new Quizdetail { QuizId = 6, CategoryId = 1, LevelId = 1, EmployeeId = 2, CompanyId = 1, ExpiresOn = DateTime.Now, Status = "Active", TotalScore = 100, SecureScore = 0, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedBy = 1, ModifiedOn = DateTime.Now });
            _dbContext.SaveChanges();

            var quiz = _quizdetailService.GetQuizdetailByIdAsync(6);

            Assert.IsTrue(quiz.Result.QuizId == 6);
            Assert.IsTrue(quiz.Result.IsActive == 1);
            Assert.IsTrue(quiz.Result.EmployeeId == 2);
            Assert.IsTrue(quiz.Result.CreatedBy == 1);
        }
        [TestMethod]
        public void InsertQuizTest()
        {
            var quiz = _quizdetailService.InsertQuizdetailAsync(new Quizdetail() { QuizId = 7, CategoryId = 1, LevelId = 2, EmployeeId = 2, CompanyId = 1, ExpiresOn = DateTime.Now, Status = "Active", TotalScore = 100, SecureScore = 0, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedBy = 1, ModifiedOn = DateTime.Now });
            var quizinsert = _dbContext.Quizdetails.Where(x => x.QuizId == 7).First();
            Assert.IsTrue(quiz.Result);
            Assert.IsTrue(quizinsert.Status == "Active");
            Assert.IsTrue(quizinsert.LevelId==2);
        }
        [TestMethod]
        public void UpdateQuizTest()
        {
            var row = _quizdetailService.InsertQuizdetailAsync(new Quizdetail() { QuizId = 8, CategoryId = 1, LevelId = 2, EmployeeId = 2, CompanyId = 1, ExpiresOn = DateTime.Now, Status = "Active", TotalScore = 100, SecureScore = 0, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedBy = 1, ModifiedOn = DateTime.Now });
            var obj = _dbContext.Quizdetails.Where(x => x.QuizId == 8).First();

            obj.IsActive = 0;
            obj.CategoryId = 2;

            var quiz = _quizdetailService.UpdateQuizdetailAsync(obj);
            var updatequiz = _dbContext.Quizdetails.Where(x => x.QuizId == 8).First();

            Assert.IsTrue(updatequiz.IsActive == 0);
            Assert.IsTrue(updatequiz.CategoryId == 2);
            Assert.IsTrue(updatequiz.CategoryId != 1);
            Assert.IsTrue(quiz.Result);
        }
        [TestMethod]
        public void DeleteQuizDetailTest()
        {
            var row = _quizdetailService.InsertQuizdetailAsync(new Quizdetail() { QuizId = 10, CategoryId = 1, LevelId = 2, EmployeeId = 2, CompanyId = 1, ExpiresOn = DateTime.Now, Status = "Active", TotalScore = 100, SecureScore = 0, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedBy = 1, ModifiedOn = DateTime.Now });
            var obj = _dbContext.Quizdetails.Where(x => x.QuizId == 10).First();

            var delete = _quizdetailService.DeleteQuizDetailsAsync(obj);
            Assert.IsTrue(delete.Result);
        }
    }
}