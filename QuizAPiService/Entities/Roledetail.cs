using System;
using System.Collections.Generic;

namespace QuizAPiService.Entities
{
    public partial class Roledetail
    {
        public Roledetail()
        {
            Userroles = new HashSet<Userrole>();
        }

        public int RoleId { get; set; }
        public string? RoleDescription { get; set; }
        public sbyte IsActive { get; set; }
        public string? Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
