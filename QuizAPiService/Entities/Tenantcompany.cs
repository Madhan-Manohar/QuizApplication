using System;
using System.Collections.Generic;

namespace QuizAPiService.Entities
{
    public partial class Tenantcompany
    {
        public Tenantcompany()
        {
            Userdetails = new HashSet<Userdetail>();
            Userroles = new HashSet<Userrole>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? CompanyCode { get; set; }
        public string? Pannumber { get; set; }
        public string? Tannumber { get; set; }
        public string? Pfreg { get; set; }
        public string? Esireg { get; set; }
        public string? State { get; set; }
        public string? MobileNumber { get; set; }
        public int TenantId { get; set; }
        public sbyte IsActive { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? Pincode { get; set; }
        public string? Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }

        public virtual Tenantmaster Tenant { get; set; } = null!;
        public virtual ICollection<Userdetail> Userdetails { get; set; }
        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
