//using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
