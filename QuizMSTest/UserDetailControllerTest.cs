using Microsoft.Extensions.Logging;
using QuizAPiService.Entities;
using QuizAPiService.Service;
using QuizAPiService.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuizMSTest
{
    [TestClass]
    public class UserDetailControllerTest
    {
        private readonly QuizContext _dbContext;

        private readonly IUserDetail _quizRepository;
        private readonly ILogger<UserDetailRepository> _logger;



        public UserDetailControllerTest() //constructor 
        {
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _quizRepository = new UserDetailRepository(_dbContext, _logger);

        }

        [TestMethod]
        public void GetRoleDetailTest()
        {
            var items = new List<Userdetail>
            {
                new Userdetail { UserId = 1,IsActive = 1, CreatedBy = "1",EmployeeId="1",EmployeeName = "sample", CreatedOn = DateTime.Now },
                new Userdetail { UserId = 2, IsActive = 1, CreatedBy = "1",EmployeeId="1",EmployeeName = "sample", CreatedOn = DateTime.Now}
            };

            _dbContext.Userdetails.AddRange(items);
            _dbContext.SaveChanges();
            var level = _quizRepository.GetUserdetails();//get request 
            var levelone = level.Result.First(i => i.UserId == 1);
            var leveltwo = level.Result.First(i => i.UserId == 2);
            Assert.IsTrue(levelone.UserId == 1);
            Assert.IsTrue(levelone.IsActive == 1);
            Assert.IsTrue(levelone.CreatedBy == "1");
            Assert.IsTrue(leveltwo.UserId == 2);
            Assert.IsTrue(leveltwo.IsActive == 1);
            Assert.IsTrue(leveltwo.CreatedBy == "1");

        }

       
        [TestMethod]
        public void GetActiveQuestionsTest()
        {
            var users = new List<Userdetail>  {
                new Userdetail { UserId = 13,IsActive = 1, CreatedBy = "1",EmployeeId="1",EmployeeName = "sample", CreatedOn = DateTime.Now },
                new Userdetail { UserId = 14, IsActive = 0, CreatedBy = "1",EmployeeId="1",EmployeeName = "sample", CreatedOn = DateTime.Now }
            };
            ;

            _dbContext.Userdetails.AddRange(users);
            _dbContext.SaveChanges();
            var getusers = _dbContext.Userdetails.Where(x => x.IsActive == 1).First(); 
            Assert.IsTrue(getusers.UserId  == 13); 
            Assert.IsTrue(getusers.EmployeeName == "sample"); 
            Assert.IsTrue(getusers.CreatedBy == "1");
        }




        [TestMethod]
        public void DeleteQuestionTest()
        {
            var user = new Userdetail { UserId = 8, IsActive = 1, CreatedBy = "1", EmployeeId = "1", EmployeeName = "sample", CreatedOn = DateTime.Now };
            var userInsert = _quizRepository.InsertUserDetail(user);



            //    var latestLevel =_dbContext.Levels.OrderByDescending(x => x.LevelId).Take(1).First();
            var latestuser = _dbContext.Userdetails.OrderByDescending(x => x.UserId).Last();
            var questionLatest = _quizRepository.GetUserDetailByUserId(latestuser.UserId);
            var questionDelete = _quizRepository.DeleteUserDetail(questionLatest.Result);
            Assert.IsTrue(questionDelete.Result);
        }

        [TestMethod]
        public void GetRoleDetailsByIdTest()
        {
            _dbContext.Userdetails.Add(new Userdetail { UserId = 3, IsActive = 1, CreatedBy = "1", EmployeeId = "1", EmployeeName = "sample", CreatedOn = DateTime.Now });
            _dbContext.SaveChanges();

            var level = _quizRepository.GetUserDetailByUserId(3);

            Assert.IsTrue(level.Result.UserId == 3);
            Assert.IsTrue(level.Result.IsActive == 1);
            Assert.IsTrue(level.Result.CreatedBy == "1");
        }


        [TestMethod]
        public void GetRoleDetailsByEmployeeIdTest()
        {
            _dbContext.Userdetails.Add(new Userdetail { UserId = 10, IsActive = 1, CreatedBy = "1", EmployeeId = "1", EmployeeName = "sample1", CreatedOn = DateTime.Now });
            _dbContext.SaveChanges();

            var level = _quizRepository.GetUserDetailByEmployeeeId("1");

            Assert.IsTrue(level.Result.UserId == 10);
            Assert.IsTrue(level.Result.IsActive == 1);
            Assert.IsTrue(level.Result.CreatedBy == "1");
        }




        [TestMethod]
        public void InsertRoleDetailTest()
        {
            var level = _quizRepository.InsertUserDetail(new Userdetail { UserId = 4, IsActive = 1, CreatedBy = "1", EmployeeId = "1", EmployeeName = "sample", CreatedOn = DateTime.Now });
            Assert.IsTrue(level.Result);
        }


      

        [TestMethod]
        public void UpdateRoleDetailLevelTest()
        {
          



            var UserInsert = _quizRepository.InsertUserDetail(new Userdetail() { UserId = 6, IsActive = 0, CreatedBy = "1", EmployeeId = "1", EmployeeName = "sample", CreatedOn = DateTime.Now });
                var insertuser = _dbContext.Userdetails.Where(x => x.UserId  == 6).First();
                insertuser.EmployeeId = "1";
                insertuser.IsActive = 1;
                insertuser.CreatedBy = "1";

                var question = _quizRepository.UpdateUserDetail(insertuser);

                var updateuser = _dbContext.Userdetails.Where(x => x.UserId == 6).First();
                Assert.IsTrue(updateuser.EmployeeId == "1");
                Assert.IsTrue(updateuser.IsActive == 1);
            }
        }



   
}

