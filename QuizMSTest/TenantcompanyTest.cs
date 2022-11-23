using Microsoft.Extensions.Logging;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using QuizAPiService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApiTesting
{
    [TestClass]
    public class TenantcompanyTest
    {
        private readonly QuizContext _dbContext;
        private readonly ITenantcompanyService _iservice;
        private readonly ILogger<TenantcompanyService> _logger;
        public TenantcompanyTest()
        {
            _dbContext = new InMemoryDbContextFactory().GetDbContext();
            _iservice = new TenantcompanyService(_dbContext, _logger);
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
            var items = new List<Tenantcompany>
            {
                new Tenantcompany { CompanyId=1,CompanyName="ABC",CompanyCode = "ABCK",Pannumber="BVBVBV123",Tannumber="BVBVBV123",Pfreg="qwe",Esireg="BHG",State = "TS",MobileNumber="9849003526",TenantId=1, IsActive=1,Address1="ACVGF",Address2="ACVGF",Address3="ACVGF",Pincode="1254",Remarks="ASD",CreatedBy=1,CreatedOn=DateTime.Now },
                new Tenantcompany { CompanyId=2,CompanyName="DEF",CompanyCode = "QAWS",Pannumber="QWERDSA",Tannumber="LKJHGF",Pfreg="qwsedr",Esireg="GHF",State = "AP",MobileNumber="95559595959",TenantId=2, IsActive=1,Address1="ACVGF",Address2="ACVGF",Address3="ACVGF",Pincode="1254",Remarks="ASD",CreatedBy=1,CreatedOn=DateTime.Now },
            };
            _dbContext.Tenantcompanies.AddRange(items);
            _dbContext.SaveChanges();

            var get = _iservice.GetTCsAsync();

            var one = _dbContext.Tenantcompanies.Where(i => i.CompanyId == 1).First();
            var two = _dbContext.Tenantcompanies.Where(i => i.CompanyId == 2).First();

            Assert.IsTrue(one.CompanyName == "ABC");
            Assert.IsTrue(two.CompanyId != 1);
            Assert.IsTrue(two.CompanyName == "DEF");
        }
        [TestMethod]
        public void GetTCByIdTest()
        {
            _dbContext.Tenantcompanies.Add(new Tenantcompany() { CompanyId = 3, CompanyName = "ABC", CompanyCode = "ABCK", Pannumber = "BVBVBV123", Tannumber = "BVBVBV123", Pfreg = "qwe", Esireg = "BHG", State = "TS", MobileNumber = "9849003526", TenantId = 1, IsActive = 1, Address1 = "ACVGF", Address2 = "ACVGF", Address3 = "ACVGF", Pincode = "1254", Remarks = "ASD", CreatedBy = 1, CreatedOn = DateTime.Now });
            _dbContext.SaveChanges();

            var obj = _iservice.GetTCByIdAsync(3);

            Assert.IsTrue(obj.Result.CompanyId == 3);
            Assert.IsTrue(obj.Result.IsActive == 1);
            Assert.IsTrue(obj.Result.Pannumber == "BVBVBV123");
        }
        [TestMethod]
        public void InsertTenantcompanyTest()
        {
            var row = _iservice.InsertTCAsync(new Tenantcompany() { CompanyId = 4, CompanyName = "Four", CompanyCode = "ABCK", Pannumber = "BVBVBV123", Tannumber = "BVBVBV123", Pfreg = "qwe", Esireg = "BHG", State = "TS", MobileNumber = "9849003526", TenantId = 1, IsActive = 1, Address1 = "ACVGF", Address2 = "ACVGF", Address3 = "ACVGF", Pincode = "1254", Remarks = "ASD", CreatedBy = 1, CreatedOn = DateTime.Now });
            var insert = _dbContext.Tenantcompanies.Where(x => x.CompanyId == 4).First();
            Assert.IsTrue(row.Result);
            Assert.IsTrue(insert.CompanyName == "Four");
            Assert.IsTrue(insert.CompanyId == 4);
        }
        [TestMethod]
        public void UpdateTeanantCompanyTest()
        {
            var row = _iservice.InsertTCAsync(new Tenantcompany() { CompanyId = 5, CompanyName = "Five", CompanyCode = "QAQA", Pannumber = "BVBVBV123", Tannumber = "BVBVBV123", Pfreg = "qwe", Esireg = "BHG", State = "TS", MobileNumber = "9849003526", TenantId = 1, IsActive = 1, Address1 = "ACVGF", Address2 = "ACVGF", Address3 = "ACVGF", Pincode = "1254", Remarks = "ASD", CreatedBy = 1, CreatedOn = DateTime.Now });
            var obj = _dbContext.Tenantcompanies.Where(x => x.CompanyId == 5).First();

            obj.IsActive = 0;
            obj.CompanyName = "five";

            var tenant = _iservice.UpdateTCAsync(obj);
            var update = _dbContext.Tenantcompanies.Where(x => x.CompanyId == 5).First();

            Assert.IsTrue(update.IsActive == 0);
            Assert.IsTrue(update.CompanyName == "five");
            Assert.IsTrue(update.IsActive != 1);
            Assert.IsTrue(tenant.Result);
        }
        [TestMethod]
        public void DeleteTC()
        {
            var row = _iservice.InsertTCAsync(new Tenantcompany() { CompanyId = 6, CompanyName = "Six", CompanyCode = "QAQA", Pannumber = "BVBVBV123", Tannumber = "BVBVBV123", Pfreg = "qwe", Esireg = "BHG", State = "TS", MobileNumber = "9849003526", TenantId = 1, IsActive = 1, Address1 = "ACVGF", Address2 = "ACVGF", Address3 = "ACVGF", Pincode = "1254", Remarks = "ASD", CreatedBy = 1, CreatedOn = DateTime.Now });
            var obj = _dbContext.Tenantcompanies.Where(x => x.CompanyId == 6).First();

            var delete = _iservice.DeleteTCsAsync(obj);
            Assert.IsTrue(delete.Result);
        }
    }
}