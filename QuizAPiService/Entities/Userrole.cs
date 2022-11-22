using System;
using System.Collections.Generic;

namespace QuizAPiService.Entities
{
    public partial class Userrole
    {
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public sbyte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Tenantcompany Company { get; set; } = null!;
        public virtual Roledetail Role { get; set; } = null!;
    }
}
