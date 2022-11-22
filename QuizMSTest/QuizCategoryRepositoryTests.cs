using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizAPiService.Entities;
using QuizAPiService.Service;
using QuizAPiService.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuizMSTest
{
    [TestClass]
    public class QuizCategoryRepositoryTests
    {
        private readonly QuizContext _dbContext;
        private readonly ICategoryQuestionService _quizRepository;
        

        public QuizCategoryRepositoryTests()
        {
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _quizRepository = new CategoryQuestionService(_dbContext);

        }

        [TestMethod]
        public void GetCategoryQuestionsTest()
        {
            var items = new List<CategoryQuestion>
            {
                new CategoryQuestion {  CategoryType = "Webserices", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "RestAPi", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            _dbContext.CategoryQuestions.AddRange(items);
            _dbContext.SaveChanges();

            var getCategoryQuestion = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "RestAPi".ToLower()).First();
            Assert.IsTrue(getCategoryQuestion.CreatedBy == 1);
            Assert.IsTrue(getCategoryQuestion.CategoryType == "RestAPi");
            Assert.IsTrue(getCategoryQuestion.IsActive == 0);
        }

        [TestMethod]
        public void GetActiveCategoryQuestionsTest()
        {
            var items = new List<CategoryQuestion>
            {
                new CategoryQuestion {  CategoryType = "SQLDb", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "MySqlDb", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            _dbContext.CategoryQuestions.AddRange(items);
            _dbContext.SaveChanges();

            var getCategoryQuestion = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "SQLDb".ToLower()).First();
            Assert.IsTrue(getCategoryQuestion.CreatedBy == 1);
            Assert.IsTrue(getCategoryQuestion.CategoryType == "SQLDb");
            Assert.IsTrue(getCategoryQuestion.IsActive == 1);
        }


        [TestMethod]
        public void GetCategoryQuestionByIdTest()
        {
            _dbContext.CategoryQuestions.Add(new CategoryQuestion {  CategoryType = "React1", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            _dbContext.SaveChanges();

            var getCategoryQuestion = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "React1".ToLower()).First();
            Assert.IsTrue(getCategoryQuestion.CreatedBy == 1);
            Assert.IsTrue(getCategoryQuestion.CategoryType == "React1");
            Assert.IsTrue(getCategoryQuestion.IsActive == 1);
        }

        [TestMethod]
        public void GetActiveCategoryQuestionByIdTest()
        {
            _dbContext.CategoryQuestions.Add(new CategoryQuestion {  CategoryType = "Angular1", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            _dbContext.SaveChanges();


            var getCategoryQuestion = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "Angular1".ToLower()).First();
            Assert.IsTrue(getCategoryQuestion.CreatedBy == 1);
            Assert.IsTrue(getCategoryQuestion.CategoryType == "Angular1");
            Assert.IsTrue(getCategoryQuestion.IsActive == 1);


        }

        [TestMethod]
        public void InsertCategoryQuestionTest()
        {
            var level = _quizRepository.InsertCategoryQuestionAsync(new CategoryQuestion() { CategoryType = "KnockOut", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            var insertCategoryQuestion = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "KnockOut".ToLower()).First();
            Assert.IsTrue(level.Result);
            Assert.IsTrue(insertCategoryQuestion.CategoryType == "KnockOut");
           Assert.IsTrue(insertCategoryQuestion.IsActive == 1);

        }
        [TestMethod]
        public void InsertCategoryQuestionsTest()
        {
            var level = _quizRepository.InsertCategoryQuestionAsync(new CategoryQuestion() { CategoryType = "TSql", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });

            var categories = new List<CategoryQuestion>
            {
                new CategoryQuestion {  CategoryType = "Sql", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "Oracle", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "SqlServer", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            var levelInsert = _quizRepository.InsertCategoryQuestionAsync(categories);


            var insertCategoryQuestion1 = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "Sql".ToLower()).First();
            var insertCategoryQuestion2 = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "Oracle".ToLower()).First();
            var insertCategoryQuestion3 = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "SqlServer".ToLower()).First();

            Assert.IsTrue(insertCategoryQuestion1.CategoryType == "Sql");
            Assert.IsTrue(insertCategoryQuestion2.CategoryType == "Oracle");
            Assert.IsTrue(insertCategoryQuestion3.CategoryType == "SqlServer");
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void InsertDuplicateCategoryQuestionTest()
        {
            var categoryQuestion = _quizRepository.InsertCategoryQuestionAsync(new CategoryQuestion() { CategoryType = "MYsql", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            var duplicateCategoryQuestion = _quizRepository.InsertCategoryQuestionAsync(new CategoryQuestion() { CategoryType = "MYsql", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            Assert.IsFalse(duplicateCategoryQuestion.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void InsertCategoryDuplicateSingleTest()
        {
            var categories = new List<CategoryQuestion>
            {
                new CategoryQuestion {  CategoryType = "Sql12", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "Oracle12", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "SqlServer12", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            var categoryQuestionInsert = _quizRepository.InsertCategoryQuestionAsync(categories);

            var insertCategory1 = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "Sql12".ToLower()).First();
            var insertCategory2 = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "Oracle12".ToLower()).First();
            var insertCategory3 = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "SqlServer12".ToLower()).First();

            Assert.IsTrue(insertCategory1.CategoryType == "Sql12");
            Assert.IsTrue(insertCategory2.CategoryType == "Oracle12");
            Assert.IsTrue(insertCategory3.CategoryType == "SqlServer12");

            var category = _quizRepository.InsertCategoryQuestionAsync(new CategoryQuestion() { CategoryType = "Sql12", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            Assert.IsFalse(category.Result);

        }
       
        [TestMethod]
        public void UpdateCategoryQuestionTest()
        {
            var levelInsert = _quizRepository.InsertCategoryQuestionAsync(new CategoryQuestion() { CategoryType = "WebApi", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            var insertCategoryQuestion = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "WebApi".ToLower()).First();
            insertCategoryQuestion.ModifiedBy = 1;
            insertCategoryQuestion.IsActive = 0;
            insertCategoryQuestion.CategoryType = "Wcf";
            insertCategoryQuestion.ModifiedOn = DateTime.Now;
            var levelCategry = _quizRepository.UpdateCategoryQuestionAsync(insertCategoryQuestion);
            //var updateCategoryQuestions = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "Wcf".ToLower()).First();
            //Assert.IsTrue(updateCategoryQuestions.CategoryType == "Wcf");
            //Assert.IsTrue(updateCategoryQuestions.IsActive == 0);
            Assert.IsTrue(levelCategry.Result);
        }

        [TestMethod]
        public void UpdateCategoryQuestionsTest()
        {
            var level = new CategoryQuestion { CategoryType = "TestCategoryQuestion", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };
            var levelInsrt = _quizRepository.InsertCategoryQuestionAsync(level);
            var levels = new List<CategoryQuestion>
            {
                new CategoryQuestion {  CategoryType = "Standard1", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "Standard2", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "Standard3", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            var levelInsert = _quizRepository.InsertCategoryQuestionAsync(levels);

            var distinctCategoryQuestions = _dbContext.CategoryQuestions.Select(y => y);
            List<CategoryQuestion> lstGetCategoryQuestions = new List<CategoryQuestion>();
            foreach (CategoryQuestion levl in distinctCategoryQuestions)
            {
                lstGetCategoryQuestions.Add(_dbContext.CategoryQuestions.Where(x => x.CategoryId == levl.CategoryId).First<CategoryQuestion>());
            }

            lstGetCategoryQuestions[0].CategoryType = "Professional1";
            lstGetCategoryQuestions[1].CategoryType = "Professional2";
            lstGetCategoryQuestions[2].CategoryType = "Professional3";
            lstGetCategoryQuestions[0].ModifiedBy = 1;
            lstGetCategoryQuestions[1].ModifiedBy = 2;
            lstGetCategoryQuestions[2].ModifiedBy = 3;
            lstGetCategoryQuestions[0].ModifiedOn = DateTime.Now;
            lstGetCategoryQuestions[1].ModifiedOn = DateTime.Now;
            lstGetCategoryQuestions[2].ModifiedOn = DateTime.Now;
            lstGetCategoryQuestions[1].IsActive = 0;

            var updatedCategoryQuestions = _quizRepository.UpdateCategoryQuestionAsync(lstGetCategoryQuestions);
            var updateCategoryQuestion1 = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "Professional1".ToLower()).First();
            var updateCategoryQuestion2 = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "Professional2".ToLower()).First();
            var updateCategoryQuestion3 = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == "Professional3".ToLower()).First();

            Assert.IsTrue(updateCategoryQuestion1.CategoryType == "Professional1");
            Assert.IsTrue(updateCategoryQuestion2.CategoryType == "Professional2");
            Assert.IsTrue(updateCategoryQuestion3.CategoryType == "Professional3");
            Assert.IsTrue(updateCategoryQuestion2.IsActive == 0);

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateDuplicateCategoryQuestionTest()
        {
            var categoryQuestion = new CategoryQuestion { CategoryType = "TestDuplicateCategoryQuestion", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };

            var levelInsert = _quizRepository.InsertCategoryQuestionAsync(categoryQuestion);
            CategoryQuestion lvl = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == categoryQuestion.CategoryType.ToLower()).First<CategoryQuestion>();


            lvl.CategoryType = "TestDuplicateCategoryQuestion";

            var updatedCategoryQuestions = _quizRepository.UpdateCategoryQuestionAsync(lvl);
            Assert.IsFalse(updatedCategoryQuestions.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateDuplicateCategoryQuestionsTest()
        {
            var categorys = new List<CategoryQuestion>
            {
                new CategoryQuestion {  CategoryType = "React2", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "Angular2", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "Node2", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            var categoryQuestionInsert = _quizRepository.InsertCategoryQuestionAsync(categorys);

            var distinctCategorys = _dbContext.CategoryQuestions.Select(y => y);
            List<CategoryQuestion> lstGetCategorys = new List<CategoryQuestion>();
            foreach (CategoryQuestion levl in distinctCategorys)
            {
                lstGetCategorys.Add(_dbContext.CategoryQuestions.Where(x => x.CategoryId == levl.CategoryId).First<CategoryQuestion>());
            }

            lstGetCategorys[0].CategoryType = "React2";
            lstGetCategorys[1].CategoryType = "Angular2";
            lstGetCategorys[2].CategoryType = "Node2";

            var updatedCategorys = _quizRepository.UpdateCategoryQuestionAsync(lstGetCategorys);
            Assert.IsFalse(updatedCategorys.Result);

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateDuplicateCategoryTest()
        {
            var level = new CategoryQuestion { CategoryType = "TestDuplicateCategory", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };

            var levelInsert = _quizRepository.InsertCategoryQuestionAsync(level);
            CategoryQuestion lvl = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() == level.CategoryType.ToLower()).First<CategoryQuestion>();


            lvl.CategoryType = "TestDuplicateCategory";

            var updatedCategorys = _quizRepository.UpdateCategoryQuestionAsync(lvl);
            Assert.IsFalse(updatedCategorys.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DeleteCategoryQuestionsTest()
        {
            var lstCategorys = new List<CategoryQuestion>
            {
                new CategoryQuestion {  CategoryType = "Java", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = ".net", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new CategoryQuestion {  CategoryType = "Cobol", IsActive = 1, CreatedBy = 3, CreatedOn = DateTime.Now }
            };
            var categoryQuestions = new CategoryQuestion { CategoryType = "TestCategory", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };
            var categoryQuestionInsrt = _quizRepository.InsertCategoryQuestionAsync(categoryQuestions);
            var categoryQuestionInsert = _quizRepository.InsertCategoryQuestionAsync(lstCategorys);
            var distinctCategorys = _dbContext.CategoryQuestions.Where(x => x.CategoryType.ToLower() != categoryQuestions.CategoryType.ToLower()).Select(y => y);
            List<CategoryQuestion> lstGetCategorys = new List<CategoryQuestion>();
            foreach (CategoryQuestion categoryQuestion in distinctCategorys)
            {
                lstGetCategorys.Add(_dbContext.CategoryQuestions.Where(x => x.CategoryId == categoryQuestion.CategoryId).First<CategoryQuestion>());
            }

            var categoryQuestionDelete = _quizRepository.DeleteCategoryQuestionAsync(lstGetCategorys);
            Assert.IsTrue(categoryQuestionDelete.Result);
        }

        [TestMethod]
      //  [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCategoryQuestionNotExistTest()
        {
            try
            {
                var categoryQuestions = new CategoryQuestion { CategoryType = "React", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };
                var categoryQuestionInsert = _quizRepository.InsertCategoryQuestionAsync(categoryQuestions);
                List<int> lstDelete = new List<int>() { 1000, 1001 };
                var categoryQuestionDelete = _quizRepository.DeleteCategoryQuestionAsync(categoryQuestions);
            }
            catch (ArgumentNullException ae)
            {
                Assert.AreEqual("Parameter cannot be null or empty.", ae.Message);
            }
;
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DeleteCategoryQuestionTest()
        {
            var categoryQuestion = new CategoryQuestion { CategoryType = "EasyMdium", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };
            var categoryQuestionInsert = _quizRepository.InsertCategoryQuestionAsync(categoryQuestion);

         //   var latestCategory = _dbContext.CategoryQuestions.OrderByDescending(x => x.CategoryId).Take(1).First();
            var latestCategory = _dbContext.CategoryQuestions.OrderByDescending(x => x.CategoryId).Last();
            var categoryQuestionLatest = _quizRepository.GetCategoryQuestionByTypeAsync(latestCategory.CategoryType);
            var categoryQuestionDelete = _quizRepository.DeleteCategoryQuestionAsync(categoryQuestionLatest.Result);
            Assert.IsTrue(categoryQuestionDelete.Result);
        }
    }

}

