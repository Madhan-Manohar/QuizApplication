using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Utilities.Collections;
using QuizAPiService.Entities;
using QuizAPiService.Service;
using QuizAPiService.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace QuizApiTesting
{
    [TestClass]
    public class TenantmasterTest
    {
        private readonly QuizContext _dbContext;
        private readonly ITenantmasterService _tenantmaster;
        private readonly ILogger<TenantmasterService> _logger;

        public TenantmasterTest()
        {
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _tenantmaster = new TenantmasterService(_dbContext, _logger);
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
        public void Get()
        {
            var items = new List<Tenantmaster>
            {
                new Tenantmaster { TenantId = 1,TenantName="VKY",Pannumber="BVBVBV123",Tannumber="BVBVBV123",Pfreg="qwe",Esireg="BHG",IsActive=1,CompanyLo="ABC",PasswordExpiry=1,Address1="ACVGF",Address2="ACVGF",Address3="ACVGF",Pincode="1254",Remarks="ASD",CreatedBy=1,CreatedOn=DateTime.Now },
                new Tenantmaster { TenantId = 2,TenantName="ABC",Pannumber="QTYTREDG",Tannumber="BVBVBV123",Pfreg="qwe",Esireg="BHG",IsActive=1,CompanyLo="ABC",PasswordExpiry=1,Address1="ACVGF",Address2="ACVGF",Address3="ACVGF",Pincode="1254",Remarks="ASD",CreatedBy=1,CreatedOn=DateTime.Now }
            };
            _dbContext.Tenantmasters.AddRange(items);
            _dbContext.SaveChanges();

            var get = _tenantmaster.GetTMsAsync();

            var one = _dbContext.Tenantmasters.Where(i => i.TenantId == 1).First();
            var two = _dbContext.Tenantmasters.Where(i => i.TenantId == 2).First();

            Assert.IsTrue(one.TenantName == "VKY");
            Assert.IsTrue(two.TenantId != 1);
            Assert.IsTrue(two.TenantName == "ABC");
        }
        [TestMethod]
        public void GetTMByIdTest()
        {
            _dbContext.Tenantmasters.Add(new Tenantmaster() { TenantId = 8, TenantName = "VM", Pannumber = "QWSED123", Tannumber = "BVBVBV123", Pfreg = "qwe", Esireg = "BHG", IsActive = 1, CompanyLo = "ABC", PasswordExpiry = 1, Address1 = "ACVGF", Address2 = "ACVGF", Address3 = "ACVGF", Pincode = "1254", Remarks = "ASD", CreatedBy = 2, CreatedOn = DateTime.Now });
            _dbContext.SaveChanges();

            var obj = _tenantmaster.GetTMByIdAsync(8);

            Assert.IsTrue(obj.Result.TenantId == 8);
            Assert.IsTrue(obj.Result.IsActive == 1);
            Assert.IsTrue(obj.Result.Pannumber == "QWSED123");
            Assert.IsTrue(obj.Result.CreatedBy == 2);
        }
        [TestMethod]
        public void InsertTenantmastersTest()
        {
            var row = _tenantmaster.InsertTMAsync(new Tenantmaster() { TenantId = 3, TenantName = "GA", Pannumber = "QWSED123", Tannumber = "BVBVBV123", Pfreg = "qwe", Esireg = "BHG", IsActive = 1, CompanyLo = "ABC", PasswordExpiry = 1, Address1 = "ACVGF", Address2 = "ACVGF", Address3 = "ACVGF", Pincode = "1254", Remarks = "ASD", CreatedBy = 1, CreatedOn = DateTime.Now });
            var insert = _dbContext.Tenantmasters.Where(x => x.TenantId == 3).First();
            Assert.IsTrue(row.Result);
            Assert.IsTrue(insert.TenantName == "GA");
            Assert.IsTrue(insert.TenantId == 3);
        }
        [TestMethod]
        public void UpdateTeanantMasterTest()
        {
            var row = _tenantmaster.InsertTMAsync(new Tenantmaster() { TenantId = 4, TenantName = "VM", Pannumber = "QWSED123", Tannumber = "BVBVBV123", Pfreg = "qwe", Esireg = "BHG", IsActive = 1, CompanyLo = "ABC", PasswordExpiry = 1, Address1 = "ACVGF", Address2 = "ACVGF", Address3 = "ACVGF", Pincode = "1254", Remarks = "ASD", CreatedBy = 1, CreatedOn = DateTime.Now });
            var obj = _dbContext.Tenantmasters.Where(x => x.TenantId == 4).First();

            obj.IsActive = 0;
            obj.TenantName = "VMA";

            var tenant = _tenantmaster.UpdateTMAsync(obj);
            var update = _dbContext.Tenantmasters.Where(x => x.TenantId == 4).First();

            Assert.IsTrue(update.IsActive == 0);
            Assert.IsTrue(update.TenantName == "VMA");
            Assert.IsTrue(update.IsActive != 1);
            Assert.IsTrue(tenant.Result);
        }
        [TestMethod]
        public void DeleteTM()
        {
            var row = _tenantmaster.InsertTMAsync(new Tenantmaster() { TenantId = 5, TenantName = "MM", Pannumber = "QWSED123", Tannumber = "BVBVBV123", Pfreg = "qwe", Esireg = "BHG", IsActive = 1, CompanyLo = "ABC", PasswordExpiry = 1, Address1 = "ACVGF", Address2 = "ACVGF", Address3 = "ACVGF", Pincode = "1254", Remarks = "ASD", CreatedBy = 1, CreatedOn = DateTime.Now });
            var obj = _dbContext.Tenantmasters.Where(x => x.TenantId == 5).First();

            var delete = _tenantmaster.DeleteTMsAsync(obj);
            Assert.IsTrue(delete.Result);
        }
    }
}