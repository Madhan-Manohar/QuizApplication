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
        [Display(Name = "Role Description")]
        [Required(ErrorMessage = "Role Description is required")]
        [RegularExpression(@"^(?:admin|Admin|Contributer|contributer|Employee|employee)$", ErrorMessage = "Invalid Role Description (Enter Only Admin/Contributer/Employee )")]
        public string? RoleDescription { get; set; }
        [Display(Name = "Active Status")]
        [Required(ErrorMessage = "Active Status is required")]
        //[Range(0, 10, ErrorMessage = "Give Values Between 0 .. 10")]
        [RegularExpression("([0-9]+)")]
        public sbyte IsActive { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status is required")]
        [RegularExpression(@"^(?:Active|Closed)$", ErrorMessage = "Invalid Status (Enter Only Active/Closed )")]

        public string? Status { get; set; }
        [Display(Name = "Created By")]
        [Required(ErrorMessage = "Created Person Id is required")]

        public int CreatedBy { get; set; }
        [Display(Name = "Date Of Creation")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Modified By")]
        public int? ModifiedBy { get; set; }
        [Display(Name = "Modified Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }


        [JsonIgnore]
        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
