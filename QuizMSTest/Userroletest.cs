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

namespace QuizTest
{
    [TestClass]
    public class Userroletest
    {
        private readonly QuizContext _dbContext;
        private readonly IUserRoleInterface _UserRoleService;
        private readonly ILogger<UserRoleService> _logger;



        public Userroletest()
        {
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _UserRoleService = new UserRoleService(_dbContext, _logger);
        }

        [TestMethod]
        public void GetUserRoleTest()
        {
            var items = new List<Userrole>
            {
                new Userrole { UserRoleId = 1, RoleId = 1, EmployeeId = 1, CompanyId = 1, IsActive=1, CreatedBy=1, CreatedOn=DateTime.Now, ModifiedBy=1,ModifiedOn=DateTime.Now }
            };



            _dbContext.Userroles.AddRange(items);
            _dbContext.SaveChanges();
            var getUserRole = _dbContext.Userroles.Where(x => x.RoleId == 1).First();
            Assert.IsTrue(getUserRole.CreatedBy == 1);
            Assert.IsTrue(getUserRole.CompanyId == 1);
            Assert.IsTrue(getUserRole.IsActive == 1);
            var quiz = _UserRoleService.GetUserrolesAsync();
            var quizone = quiz.Result.First(i => i.UserRoleId == 1);
            // var quiztwo = quiz.Result.First(i => i.UserRoleId == 2);
            Assert.IsTrue(quizone.UserRoleId == 1);
            Assert.IsTrue(quizone.EmployeeId == 1);
            Assert.IsTrue(quizone.IsActive == 1);
            //Assert.IsTrue(quiztwo.CreatedBy == 1);
            //Assert.IsTrue(quiztwo.CompanyId == 1);
        }
        [TestMethod]
        public void GetUserRoleByIdTest()
        {
            _dbContext.Userroles.Add(new Userrole { UserRoleId = 1, RoleId = 1, EmployeeId = 1,  CompanyId = 1, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedBy = 1, ModifiedOn = DateTime.Now, });
            _dbContext.SaveChanges();
            //var getUserRole = _dbContext.Userroles.Where(x => x.RoleId == 1).First();
            //Assert.IsTrue(getUserRole.CreatedBy == 1);
            //Assert.IsTrue(getUserRole.CompanyId == 1);
            //Assert.IsTrue(getUserRole.IsActive == 1);



            var quiz = _UserRoleService.GetUserroleByIdAsync(1);



            Assert.IsTrue(quiz.Result.UserRoleId == 1);
            Assert.IsTrue(quiz.Result.IsActive == 1);
            Assert.IsTrue(quiz.Result.EmployeeId == 1);
            Assert.IsTrue(quiz.Result.CreatedBy == 1);
        }
        [TestMethod]
        public void InsertUserRoleTest()
        {
            var level = _UserRoleService.InsertUserroleAsync(new Userrole() { UserRoleId = 6, RoleId = 1, EmployeeId = 1, CompanyId = 1, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedBy = 1, ModifiedOn = DateTime.Now, SysEndTime = DateTime.Now, SysStartTime = DateTime.Now });
            Assert.IsTrue(level.Result.UserRoleId == 6);
        }
        [TestMethod]
        public void InsertUserRolesTest()
        {
            var quizdetailinsert = _UserRoleService.InsertUserroleAsync(new Userrole() { UserRoleId = 8, RoleId = 1, EmployeeId = 1, CompanyId = 1, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now, SysEndTime = DateTime.Now, SysStartTime = DateTime.Now });
            var levelone = quizdetailinsert.Result;
            Assert.IsTrue(levelone.UserRoleId == 8);
            Assert.IsTrue(levelone.EmployeeId == 1);
            Assert.IsTrue(levelone.IsActive == 1);
        }
        [TestMethod]
        public void UpdateUserRoleLevelTest()
        {
            var insert = _UserRoleService.InsertUserroleAsync(new Userrole() { UserRoleId= 9, RoleId = 1, EmployeeId = 1, CompanyId = 1, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now, SysEndTime = DateTime.Now, SysStartTime = DateTime.Now });
            var update = _UserRoleService.UpdateUserroleAsync(new Userrole() { UserRoleId = 9, RoleId = 2, EmployeeId = 2, CompanyId = 1, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now, SysEndTime = DateTime.Now, SysStartTime = DateTime.Now });



            Assert.IsTrue(update.EmployeeId == 2);
        }
        [TestMethod]
        public void DeleteUserRoleTest()
        {
            var insert = _UserRoleService.InsertUserroleAsync(new Userrole() { UserRoleId = 9, RoleId = 1, EmployeeId = 1, CompanyId = 1, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now, SysEndTime = DateTime.Now, SysStartTime = DateTime.Now });
            var Delete = _UserRoleService.DeleteUserroleAsync(insert.Result.UserRoleId);
            Assert.IsTrue(Delete);
        }
        //[TestMethod]
        //public void DeleteUserRolesTest()
        //{
        //    var insert = _UserRoleService.InsertUserroleAsync(new Userrole() { UserRoleId = 9, RoleId = 1, EmployeeId = 1, CompanyId = 1, IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now, SysEndTime = DateTime.Now, SysStartTime = DateTime.Now });
        //    int id = insert.Result.UserRoleId;
        //    var Delete = _UserRoleService.DeleteUserroleAsync(9);



        //    Assert.IsTrue(Delete);




        //}


    }
}
