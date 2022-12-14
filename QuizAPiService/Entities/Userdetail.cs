using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace QuizAPiService.Entities
{
    public partial class Userdetail
    {
        public Userdetail()
        {
            Quizdetails = new HashSet<Quizdetail>();
            Userroles = new HashSet<Userrole>();
        }

        public int UserId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public int? CompanyId { get; set; }
        public sbyte IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Tenantcompany? Company { get; set; }
        public virtual ICollection<Quizdetail> Quizdetails { get; set; }
        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
