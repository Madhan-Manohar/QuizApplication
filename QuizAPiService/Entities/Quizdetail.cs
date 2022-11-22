using System;
using System.Collections.Generic;

namespace QuizAPiService.Entities
{
    public partial class Quizdetail
    {
        public int QuizId { get; set; }
        public int CategoryId { get; set; }
        public int LevelId { get; set; }
        public int? EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public string? Status { get; set; }
        public int? TotalScore { get; set; }
        public int? SecureScore { get; set; }
        public sbyte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual CategoryQuestion Category { get; set; } = null!;
        public virtual Level Level { get; set; } = null!;
    }
}
