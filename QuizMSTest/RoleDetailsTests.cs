using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySqlX.XDevAPI.Common;
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


namespace QuizMSTest
{
    [TestClass]
    public class RoleDetailsTests
    {
        private readonly QuizContext _dbContext;
        private readonly IRoleDetailsInterfaceService _quizRepository;
        private readonly ILogger<RoleDetailsService> _logger;
        

        public RoleDetailsTests()
        {
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _quizRepository = new RoleDetailsService(_dbContext, _logger);

        }

        [TestMethod]
        public void GetRoleDetailTest()
        {
            var items = new List<Roledetail>
            {
                new Roledetail {RoleId = 1,RoleDescription = "Admin",IsActive = 1,Status = "Active",CreatedBy = 0,CreatedOn = DateTime.Now,ModifiedBy = 0,ModifiedOn=DateTime.Now},
                new Roledetail {RoleId = 2,RoleDescription = "Admin",IsActive = 1,Status = "Active",CreatedBy = 0,CreatedOn = DateTime.Now,ModifiedBy = 0,ModifiedOn=DateTime.Now}
            };

            _dbContext.Roledetails.AddRange(items);
            _dbContext.SaveChanges();
            var level = _quizRepository.GetRoleDetailsAsync();
            var levelone = level.Result.First(i => i.RoleId == 1);
            var leveltwo = level.Result.First(i => i.RoleId == 2);
            Assert.IsTrue(levelone.RoleId == 1);
            Assert.IsTrue(levelone.IsActive == 1);
            Assert.IsTrue(levelone.CreatedBy == 0);
            Assert.IsTrue(leveltwo.RoleId == 2);
            Assert.IsTrue(leveltwo.IsActive == 1);
            Assert.IsTrue(leveltwo.CreatedBy == 0);
         
        }


        [TestMethod]
        public void GetRoleDetailsByIdTest()
        {
            _dbContext.Roledetails.Add(new Roledetail { RoleId = 3, RoleDescription = "Admin", IsActive = 1, Status = "Active", CreatedBy = 0, CreatedOn = DateTime.Now, ModifiedBy = 0, ModifiedOn = DateTime.Now });
            _dbContext.SaveChanges();

            var level = _quizRepository.GetRoleDetailByIdAsync(3);

            Assert.IsTrue(level.Result.RoleId == 3);
            Assert.IsTrue(level.Result.IsActive == 1);
            Assert.IsTrue(level.Result.CreatedBy == 0);
        }


        [TestMethod]
        public void InsertRoleDetailTest()
        {
            var level = _quizRepository.InsertRoleDetailAsync(new Roledetail(){RoleId = 4,RoleDescription = "Admin",IsActive = 1,Status = "Active", CreatedBy = 0,CreatedOn = DateTime.Now,ModifiedBy = 0,ModifiedOn=DateTime.Now});
            Assert.IsTrue(level.Result.RoleId==4);
        }

        [TestMethod]
        public void InsertRoleDetailsTest()
        {

            var level = _quizRepository.InsertRoleDetailAsync(new Roledetail() { RoleId = 5, RoleDescription = "Admin", IsActive = 1, Status = "Active", CreatedBy = 0, CreatedOn = DateTime.Now, ModifiedBy = 0, ModifiedOn = DateTime.Now });


            var levelone = level.Result;
            
            Assert.IsTrue(levelone.RoleId == 5);
            Assert.IsTrue(levelone.IsActive == 1);
            Assert.IsTrue(levelone.CreatedBy == 0);

        }

        [TestMethod]
        public void UpdateRoleDetailTest()
        {
            var levelinsert = _quizRepository.InsertRoleDetailAsync(new Roledetail() { RoleId = 6, RoleDescription = "Admin", IsActive = 1, Status = "Active", CreatedBy = 0, CreatedOn = DateTime.Now, ModifiedBy = 0, ModifiedOn = DateTime.Now });
            var level = _quizRepository.UpdateRoleDetailAsync(new Roledetail() { RoleId = 6, RoleDescription = "Admin", IsActive = 1, Status = "Active", CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedBy = 0, ModifiedOn = DateTime.Now });

            Assert.IsTrue(level.CreatedBy==1);
        }



        [TestMethod]
        public void DeleteRoleDetailsTest()
        {
           
            var levelInsert = _quizRepository.InsertRoleDetailAsync(new Roledetail() { RoleId = 7, RoleDescription = "Admin", IsActive = 1, Status = "Active", CreatedBy = 0, CreatedOn = DateTime.Now, ModifiedBy = 0, ModifiedOn = DateTime.Now });

            int level = levelInsert.Result.RoleId;
            var levelDelete = _quizRepository.DeleteRoleDetailsAsync(level);

            Assert.IsTrue(levelDelete);

        }

        [TestMethod]
        public void DeleteRoleDetailFailTest()
        {

            var levelInsert = _quizRepository.InsertRoleDetailAsync(new Roledetail() { RoleId = 8, RoleDescription = "Admin", IsActive = 1, Status = "Active", CreatedBy = 0, CreatedOn = DateTime.Now, ModifiedBy = 0, ModifiedOn = DateTime.Now });
            int level = 0;
            var leveldelete = _quizRepository.DeleteRoleDetailsAsync(level);
            
            Assert.IsFalse(leveldelete);

        }
    }
}
