using K4os.Compression.LZ4;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto;
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
    public class QuizLevelRepositoryTests
    {
        private readonly QuizContext _dbContext;
        private readonly ILevelInterfaceService _quizRepository;
        

        public QuizLevelRepositoryTests()
        {
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _quizRepository = new LevelService(_dbContext);

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
        public void GetLevelsTest()
        {
            var levels = new List<Level>
            {
                new Level {  LevelType = "Easy", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "Meium", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            _dbContext.Levels.AddRange(levels);
            _dbContext.SaveChanges();
            var getLevel = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "Meium".ToLower()).First();
            Assert.IsTrue(getLevel.CreatedBy == 1);
            Assert.IsTrue(getLevel.LevelType == "Meium");
            Assert.IsTrue(getLevel.IsActive == 0);

        }

        [TestMethod]
        public void GetActiveLevelsTest()
        {
            var levels = new List<Level>
            {
                new Level {  LevelType = "Begin", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "High", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            _dbContext.Levels.AddRange(levels);
            _dbContext.SaveChanges();
            var getLevel = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "Begin".ToLower()).First();
            Assert.IsTrue(getLevel.CreatedBy == 1);
            Assert.IsTrue(getLevel.LevelType == "Begin");
            Assert.IsTrue(getLevel.IsActive == 1);
        }

        [TestMethod]
        public void GetLevelByIdTest()
        {
            _dbContext.Levels.Add(new Level { LevelType = "Excellent", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            _dbContext.SaveChanges();


            var getLevel = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "Excellent".ToLower()).First();
            Assert.IsTrue(getLevel.CreatedBy == 1);
            Assert.IsTrue(getLevel.LevelType == "Excellent");
            Assert.IsTrue(getLevel.IsActive == 1);
        }

        [TestMethod]
        public void GetActiveLevelByIdTest()
        {
            _dbContext.Levels.Add(new Level {  LevelType = "Professional", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            _dbContext.SaveChanges();

            var getLevel = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "Professional".ToLower()).First();
            Assert.IsTrue(getLevel.CreatedBy==1);
            Assert.IsTrue(getLevel.LevelType == "Professional");
            Assert.IsTrue(getLevel.IsActive == 1);
        }

        [TestMethod]
        public void InsertLevelTest()
        {
            var level = _quizRepository.InsertLevelAsync(new Level() {  LevelType = "Interediate", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            var insertLevel = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "Interediate".ToLower()).First();
            Assert.IsTrue(level.Result);
            Assert.IsTrue(insertLevel.LevelType == "Interediate");
            Assert.IsTrue(insertLevel.IsActive == 1);
  
        }
        [TestMethod]
        public void InsertLevelsTest()
        {       
            var levels = new List<Level>
            {
                new Level {  LevelType = "StdEasy", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "StdMeium", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "StdHign", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            var levelInsert = _quizRepository.InsertLevelAsync(levels);

            var insertLevel1 = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "StdEasy".ToLower()).First();
            var insertLevel2 = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "StdMeium".ToLower()).First();
            var insertLevel3 = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "StdHign".ToLower()).First();

            Assert.IsTrue(insertLevel1.LevelType == "StdEasy");
            Assert.IsTrue(insertLevel2.LevelType == "StdMeium");
            Assert.IsTrue(insertLevel3.LevelType == "StdHign");

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void InsertDuplicateLevelTest()
        {
            var level = _quizRepository.InsertLevelAsync(new Level() { LevelType = "Intermediate", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            var levelDuplicate = _quizRepository.InsertLevelAsync(new Level() { LevelType = "Intermediate", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            Assert.IsFalse(levelDuplicate.Result);

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void InsertLevelDuplicateSingleTest()
        {
            var levels = new List<Level>
            {
                new Level {  LevelType = "StddEasy", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "StddMeium", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "StddHign", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            var levelInsert = _quizRepository.InsertLevelAsync(levels);

            var insertLevel1 = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "StddEasy".ToLower()).First();
            var insertLevel2 = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "StddMeium".ToLower()).First();
            var insertLevel3 = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "StddHign".ToLower()).First();

            Assert.IsTrue(insertLevel1.LevelType == "StddEasy");
            Assert.IsTrue(insertLevel2.LevelType == "StddMeium");
            Assert.IsTrue(insertLevel3.LevelType == "StddHign");
    
            var level = _quizRepository.InsertLevelAsync(new Level() { LevelType = "StddEasy", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            Assert.IsFalse(level.Result);

        }
      
        [TestMethod]
        public void UpdateLevelTest()
        {
           var levelInsert = _quizRepository.InsertLevelAsync(new Level() {  LevelType = "Standard", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now });
            var insertLevel = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "Standard".ToLower()).First();
            insertLevel.ModifiedBy = 1;
            insertLevel.IsActive = 0;
            insertLevel.LevelType = "DifficultMedium";
            insertLevel.ModifiedOn = DateTime.Now;
           var level = _quizRepository.UpdateLevelAsync(insertLevel);
           var updateLevels = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "DifficultMedium".ToLower()).First();
            Assert.IsTrue(updateLevels.LevelType == "DifficultMedium");
            Assert.IsTrue(updateLevels.IsActive== 0);
            Assert.IsTrue(level.Result);
        }


        [TestMethod]
        public void UpdateLevelsTest()
        {
            var level = new Level { LevelType = "TestLevel", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };
            var levelInsrt = _quizRepository.InsertLevelAsync(level);
            var levels = new List<Level>
            {
                new Level {  LevelType = "Standard1", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "Standard2", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "Standard3", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            var levelInsert = _quizRepository.InsertLevelAsync(levels);

            var distinctLevels = _dbContext.Levels.Select(y => y);
            List<Level> lstGetLevels = new List<Level>();
            foreach (Level levl in distinctLevels)
            {
                lstGetLevels.Add(_dbContext.Levels.Where(x => x.LevelId == levl.LevelId).First<Level>());
            }

            lstGetLevels[0].LevelType = "Professional1";
            lstGetLevels[1].LevelType = "Professional2";
            lstGetLevels[2].LevelType = "Professional3";
            lstGetLevels[0].ModifiedBy = 1;
            lstGetLevels[1].ModifiedBy = 2;
            lstGetLevels[2].ModifiedBy = 3;
            lstGetLevels[0].ModifiedOn = DateTime.Now;
            lstGetLevels[1].ModifiedOn = DateTime.Now;
            lstGetLevels[2].ModifiedOn = DateTime.Now;
            lstGetLevels[1].IsActive = 0;

            var updatedLevels = _quizRepository.UpdateLevelAsync(lstGetLevels);
            var updateLevel1 = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "Professional1".ToLower()).First();
            var updateLevel2 = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "Professional2".ToLower()).First();
            var updateLevel3 = _dbContext.Levels.Where(x => x.LevelType.ToLower() == "Professional3".ToLower()).First();

            Assert.IsTrue(updateLevel1.LevelType == "Professional1");
            Assert.IsTrue(updateLevel2.LevelType == "Professional2");
            Assert.IsTrue(updateLevel3.LevelType == "Professional3");
            Assert.IsTrue(updateLevel2.IsActive == 0);

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateDuplicateLevelsTest()
        {
            var level = new Level { LevelType = "TestLevel", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };
            var levelInsrt = _quizRepository.InsertLevelAsync(level);
            var levels = new List<Level>
            {
                new Level {  LevelType = "Standard1", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "Standard2", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "Standard3", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            var levelInsert = _quizRepository.InsertLevelAsync(levels);

            var distinctLevels = _dbContext.Levels.Select(y => y);
            List<Level> lstGetLevels = new List<Level>();
            foreach (Level levl in distinctLevels)
            {
                lstGetLevels.Add(_dbContext.Levels.Where(x => x.LevelId == levl.LevelId).First<Level>());
            }

            lstGetLevels[1].LevelType = "Professional1";
            lstGetLevels[2].LevelType = "Standard2";
            lstGetLevels[3].LevelType = "Professional3";

            var updatedLevels = _quizRepository.UpdateLevelAsync(lstGetLevels);
            Assert.IsFalse(updatedLevels.Result);

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateDuplicateLevlsTest()
        {
            var levels = new List<Level>
            {
                new Level {  LevelType = "Standard1", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "Standard2", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "Standard3", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now }
            };

            var levelInsert = _quizRepository.InsertLevelAsync(levels);

            var distinctLevels = _dbContext.Levels.Select(y => y);
            List<Level> lstGetLevels = new List<Level>();
            foreach (Level levl in distinctLevels)
            {
                lstGetLevels.Add(_dbContext.Levels.Where(x => x.LevelId == levl.LevelId).First<Level>());
            }

            lstGetLevels[0].LevelType = "Professional1";
            lstGetLevels[1].LevelType = "Standard2";
            lstGetLevels[2].LevelType = "Professional1";

            var updatedLevels = _quizRepository.UpdateLevelAsync(lstGetLevels);
            Assert.IsFalse(updatedLevels.Result);

        }


        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void UpdateDuplicateLevelTest()
        {
            var level = new Level { LevelType = "TestDuplicateLevel", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };           

            var levelInsert = _quizRepository.InsertLevelAsync(level);
            Level lvl = _dbContext.Levels.Where(x => x.LevelType.ToLower() == level.LevelType.ToLower()).First<Level>();


            lvl.LevelType = "TestDuplicateLevel";

            var updatedLevels = _quizRepository.UpdateLevelAsync(lvl);
            Assert.IsFalse(updatedLevels.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DeleteLevelsTest()
        {
            var lstLevels = new List<Level>
            {
                new Level {  LevelType = "EsyMedium", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "MeiumEsy", IsActive = 0, CreatedBy = 1, CreatedOn = DateTime.Now },
                new Level {  LevelType = "Difficult", IsActive = 1, CreatedBy = 3, CreatedOn = DateTime.Now }
            };
            var levels = new Level { LevelType = "TestLevel", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };
            var levelInsrt =  _quizRepository.InsertLevelAsync(levels);
            var levelInsert = _quizRepository.InsertLevelAsync(lstLevels);
            var distinctLevels = _dbContext.Levels.Where(x => x.LevelType.ToLower()!= levels.LevelType.ToLower()).Select(y => y);
            List<Level> lstGetLevels = new List<Level>();
            foreach (Level level in distinctLevels)
            {
                lstGetLevels.Add(_dbContext.Levels.Where(x => x.LevelId == level.LevelId).First<Level>());
            }

            var levelDelete = _quizRepository.DeleteLevelAsync(lstGetLevels);
            Assert.IsTrue(levelDelete.Result);
        }

        [TestMethod]
        public void DeleteLevelNotExistTest()
        {
            try
            {
                var levels = new Level { LevelType = "MmEasy", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now };
                var levelInsert = _quizRepository.InsertLevelAsync(levels);
                List<int> lstDelete = new List<int>() { 1000, 1001 };
                var levelDelete = _quizRepository.DeleteLevelAsync(levels);
            }
            catch (ArgumentNullException ae)
            {
                Assert.AreEqual("Parameter cannot be null or empty.", ae.Message);
            }
}

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DeleteLevelTest()
        {
            var level =  new Level { LevelType = "EasyMdium", IsActive = 1, CreatedBy = 1, CreatedOn = DateTime.Now } ;
            var levelInsert = _quizRepository.InsertLevelAsync(level);
            var latestLevel = _dbContext.Levels.OrderByDescending(x => x.LevelId).Last();
            var levelLatest = _quizRepository.GetLevelByTypeAsync(latestLevel.LevelType);
            var levelDelete = _quizRepository.DeleteLevelAsync(levelLatest.Result);
            Assert.IsTrue(levelDelete.Result);
        }
    }
}
