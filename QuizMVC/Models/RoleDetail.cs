using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizMVC.Models
{
    public class RoleDetail
    {
        public int RoleId { get; set; }
        public string RoleDescription { get; set; }
        public sbyte IsActive { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}