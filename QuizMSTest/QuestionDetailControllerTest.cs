using Microsoft.Extensions.Logging;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using QuizAPiService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMSTest
{
    [TestClass]
    public class QuestionDetailControllerTest

    {
        private readonly QuizContext _dbContext;
        private readonly IQuestionDetails _quizRepository;
        private readonly ILogger<QuestionDetailRepository> _logger;


        public QuestionDetailControllerTest()
        {
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _quizRepository = new QuestionDetailRepository(_dbContext, _logger);

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
        public void GetQuestionDetailsTest()
        {
            var questions = new List<Questiondetail>
            {
                new Questiondetail {  QuestionDescription = "What is SQL", IsActive = 0, CategoryId = 1, QuestionId = 1, LevelId = 1 },
                new Questiondetail { QuestionDescription = "What is OOPS", IsActive = 1, CategoryId = 1, QuestionId = 2, LevelId = 2 }
            };

            _dbContext.Questiondetails.AddRange(questions);
            _dbContext.SaveChanges();
            var getquestion = _dbContext.Questiondetails.Where(x => x.QuestionDescription.ToLower() == "What is SQL".ToLower()).First();
            Assert.IsTrue(getquestion.LevelId == 1);
            Assert.IsTrue(getquestion.QuestionDescription == "What is SQL");
            Assert.IsTrue(getquestion.IsActive == 0);

        }

        [TestMethod]
        public void GetActiveQuestionsTest()
        {
            var questions = new List<Questiondetail>
            {
                new Questiondetail { QuestionDescription = "What is HTML", IsActive = 1, CategoryId = 2, QuestionId = 3, LevelId = 1 },
                new Questiondetail { QuestionDescription = "What is CSS", IsActive = 0, CategoryId = 1, QuestionId = 4, LevelId = 1 }
            };

            _dbContext.Questiondetails.AddRange(questions);
            _dbContext.SaveChanges();
            var getquestion = _dbContext.Questiondetails.Where(x => x.IsActive == 1).First();
            Assert.IsTrue(getquestion.CategoryId == 2);
            Assert.IsTrue(getquestion.QuestionDescription  == "What is HTML");
            Assert.IsTrue(getquestion.QuestionId  == 3);
        }

        [TestMethod]
        public void GetQuestionByQuestionIdTest()
        {
            _dbContext.Questiondetails.Add(new Questiondetail { QuestionDescription = "What is Jquery", IsActive = 1, CategoryId = 2, QuestionId = 5, LevelId = 1 });
            _dbContext.SaveChanges();


            var getquestion = _dbContext.Questiondetails.Where(x => x.QuestionId == 5).First();
            Assert.IsTrue(getquestion.IsActive  == 1);
            Assert.IsTrue(getquestion.CategoryId == 2);
            Assert.IsTrue(getquestion.LevelId == 1);
        }

        [TestMethod]
        public void GetQuestionByCategoryIdTest()
        {
            _dbContext.Questiondetails.Add(new Questiondetail { QuestionDescription = "What is Polymorphism", IsActive = 0, CategoryId = 1, QuestionId = 6, LevelId = 2 });
            _dbContext.SaveChanges();


            var getquestion = _dbContext.Questiondetails.Where(x => x.CategoryId == 1).First();
            Assert.IsTrue(getquestion.IsActive == 0);
            Assert.IsTrue(getquestion.QuestionDescription == "What is Polymorphism");
            Assert.IsTrue(getquestion.LevelId == 2);
        }

        [TestMethod]
        public void GetQuestionByLevelIdTest()
        {
            _dbContext.Questiondetails.Add(new Questiondetail { QuestionDescription = "What is WebApi", IsActive = 1, CategoryId = 2, QuestionId = 7, LevelId = 1 });
            _dbContext.SaveChanges();


            var getquestion = _dbContext.Questiondetails.Where(x => x.LevelId  == 1).First();
            Assert.IsTrue(getquestion.IsActive == 1);
            Assert.IsTrue(getquestion.QuestionId  == 7);
            Assert.IsTrue(getquestion.CategoryId  == 2);
        }
        [TestMethod]
        public void InsertQuestionTest()
        {
            var question = _quizRepository.InsertQuestionDetails(new Questiondetail() { QuestionDescription = "What is MsTest", IsActive = 1, CategoryId = 2, QuestionId = 8, LevelId = 1 });
            var insertquestion = _dbContext.Questiondetails.Where(x => x.QuestionDescription.ToLower() == "What is MsTest".ToLower()).First();
            Assert.IsTrue(question.Result);
            Assert.IsTrue(insertquestion.CategoryId == 2);
            Assert.IsTrue(insertquestion.IsActive == 1);

        }

        [TestMethod]
        public void InsertDuplicateQuestionTest()
        {
            var question = _quizRepository.InsertQuestionDetails(new Questiondetail() { QuestionDescription = "What is MsTest", IsActive = 1, CategoryId = 2, QuestionId = 9, LevelId = 1 });
            var questionDuplicate = _quizRepository.InsertQuestionDetails(new Questiondetail() { QuestionDescription = "What is MsTest", IsActive = 1, CategoryId = 2, QuestionId = 10, LevelId = 2 });
            Assert.IsFalse(questionDuplicate.Result);

        }

        [TestMethod]
        public void UpdateQuestionTest()
        {
            var QuestionInsert = _quizRepository.InsertQuestionDetails(new Questiondetail() { QuestionDescription = "What is Python", IsActive = 1, CategoryId = 2, QuestionId = 11, LevelId = 1 });
            var insertquestion = _dbContext.Questiondetails.Where(x => x.QuestionDescription.ToLower() == "What is Python".ToLower()).First();
            insertquestion.CategoryId  = 1;
            insertquestion.IsActive = 0;
            insertquestion.LevelId = 2;
            
            var question = _quizRepository.UpdateQuestionDetails(insertquestion);
            
            var updatequestions = _dbContext.Questiondetails.Where(x => x.QuestionDescription.ToLower() == "What is Python".ToLower()).First();
            Assert.IsTrue(updatequestions.CategoryId == 1);
            Assert.IsTrue(updatequestions.IsActive == 0);
        }

        [TestMethod]
        public void UpdateDuplicateQuestionTest()
        {
            var question = new Questiondetail { QuestionDescription = "What is SDLC", IsActive = 1, CategoryId = 3, QuestionId = 12, LevelId = 1 };

            var questionInsert = _quizRepository.InsertQuestionDetails(question);
            Questiondetail lvl = _dbContext.Questiondetails.Where(x => x.QuestionDescription.ToLower() == "What is SDLC".ToLower()).First();


            lvl.QuestionDescription = "What is SDLC";

            var updatedquestions = _quizRepository.UpdateQuestionDetails(lvl);
            Assert.IsFalse(updatedquestions.Result);
        }

        [TestMethod]
        public void DeleteQuestionTest()
        {
            var question = new Questiondetail { QuestionDescription = "What is JAVA", IsActive = 1, CategoryId = 2, QuestionId = 13, LevelId = 1 };
            var questionInsert = _quizRepository.InsertQuestionDetails(question);

            //    var latestLevel =_dbContext.Levels.OrderByDescending(x => x.LevelId).Take(1).First();
            var latestquestion = _dbContext.Questiondetails.OrderByDescending(x => x.QuestionId).Last();
            var questionLatest = _quizRepository.GetQuestionDetailByQuestionId(latestquestion.QuestionId);
            var questionDelete = _quizRepository.DeleteQuestionDetails(questionLatest.Result);
            Assert.IsTrue(questionDelete.Result);
        }

    }
}
