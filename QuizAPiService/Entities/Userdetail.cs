using System;
using System.Collections.Generic;

namespace QuizAPiService.Entities
{
    public partial class Userdetail
    {
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public int? CompanyId { get; set; }
        public sbyte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Tenantcompany? Company { get; set; }
    }
}
