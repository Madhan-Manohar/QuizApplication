using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace QuizAPiService.Entities
{
    public partial class Quizdetail
    {
        [Key]
        public int QuizId { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Level is required")]
        [Display(Name = "Level")]
        public int LevelId { get; set; }
        [Required(ErrorMessage = "Employee is required")]
        [Display(Name = "Employee")]
        public int? EmployeeId { get; set; }
        [Required(ErrorMessage = "Company is required")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "Expire Date is required")]
        [Display(Name = "Expire Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ExpiresOn { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status is required")]
        [RegularExpression(@"^(?:Active|Closed)$", ErrorMessage = "Invalid Status (Enter Only Active/Closed )")]
        public string? Status { get; set; }
        [Display(Name = "Total Score")]
        [Required(ErrorMessage = "Total Score is required")]
        public int? TotalScore { get; set; }
        [Display(Name = "Obtained Score")]
        [Required(ErrorMessage = "Obtained Score is required")]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100 only!")]
        public int? SecureScore { get; set; }
        [Display(Name = "Active Status")]
        [Required(ErrorMessage = "Active Status is required")]
        public sbyte IsActive { get; set; }
        [Display(Name = "Created By")]
        [Required(ErrorMessage = "Created Person Name is required")]
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
        public virtual CategoryQuestion? Category { get; set; }// = null!;
        [JsonIgnore]
        public virtual Level? Level { get; set; }// = null!;
    }
}