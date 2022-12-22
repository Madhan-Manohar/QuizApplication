using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuizMVC.Models
{
    public partial class Userrole
    {
        [Key]
        //[Range(0, int.MaxValue, ErrorMessage = "UserRoleId must be a positive number")]
        public int UserRoleId { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "RoleId must be a positive number")]
        public int RoleId { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "EmployeeId must be a positive number")]
        public int EmployeeId { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "EmployeeId must be a positive number")]
        public int CompanyId { get; set; }
        //[Range(0, 1, ErrorMessage = "IsActive must be a number either 1 or 0 number")]
        public sbyte IsActive { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "CreatedBy must be a positive number")]
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "ModifiedBy must be a positive number")]
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }
        

    }
}
