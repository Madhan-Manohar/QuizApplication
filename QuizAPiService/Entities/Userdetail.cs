using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace QuizAPiService.Entities
{
    public partial class Userdetail
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "UserId must be a positive number")]
        public int UserId { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "EmployeeId must be a positive number")]
        public int EmployeeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "EmployeeName is required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Employee Name should be less than 50 and greater than zero")]

        public string EmployeeName { get; set; } = null!;

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }


        [Display(Name = "Gender")]
        [RegularExpression(@"^(?:male|Male|female|Female)$", ErrorMessage = "Invalid Gender(Enter Only Male or Female)")]
        public string? Gender { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Company Id must be a positive number")]
        public int? CompanyId { get; set; }

        [Range(0, 1, ErrorMessage = "IsActive must be a number either 1 or 0 number")]
        public sbyte IsActive { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "CreatedBy must be a positive number")]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ModifiedBy must be a positive number")]
        public int? ModifiedBy { get; set; }


        public DateTime? ModifiedOn { get; set; }

        //public DateTime? SysStartTime { get; set; }
        //public DateTime? SysEndTime { get; set; }

        [JsonIgnore]
        public virtual Tenantcompany? Company { get; set; }
    }
}
