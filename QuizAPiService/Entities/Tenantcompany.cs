using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace QuizAPiService.Entities
{
    public partial class Tenantcompany
    {
        public Tenantcompany()
        {
            Userdetails = new HashSet<Userdetail>();
            Userroles = new HashSet<Userrole>();
        }

        public int CompanyId { get; set; }
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company Name is required")]
        [MaxLength(200, ErrorMessage = "Company Name should not exceed 200 characters")]
        public string? CompanyName { get; set; }
        [Display(Name = "Company Code")]
        [Required(ErrorMessage = "Company Code is required")]
        [MaxLength(50, ErrorMessage = "Company Code should not exceed 50 characters")]
        public string? CompanyCode { get; set; }
        [Display(Name = "Pan Number")]
        [Required(ErrorMessage = "Pan Number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Pan Number should have 10 characters")]
        public string? Pannumber { get; set; }
        [Display(Name = "Tan Number")]
        [Required(ErrorMessage = "Tan Number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Tan Number should have 10 characters")]
        public string? Tannumber { get; set; }
        [Display(Name = "PF Number")]
        [Required(ErrorMessage = "PF Number is required")]
        [MaxLength(50, ErrorMessage = "PF Number should not exceed 50 characters")]
        public string? Pfreg { get; set; }
        [Display(Name = "ESI Number")]
        [Required(ErrorMessage = "ESI Number is required")]
        [MaxLength(50, ErrorMessage = "ESI Number should not exceed 50 characters")]
        public string? Esireg { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required")]
        [MaxLength(100, ErrorMessage = "State should not exceed 100 characters")]
        public string? State { get; set; }
        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string? MobileNumber { get; set; }
        [Required(ErrorMessage = "Tenant is required")]
        [Display(Name = "Tenant")]
        public int TenantId { get; set; }
        [Display(Name = "Active Status")]
        [Required(ErrorMessage = "Active Status is required")]
        public sbyte IsActive { get; set; }
        [Display(Name = "Address-1")]
        [Required(ErrorMessage = "Address-1 is required")]
        [MaxLength(100, ErrorMessage = "Address should not exceed 100 characters")]
        public string? Address1 { get; set; }
        [Display(Name = "Address-2")]
        [Required(ErrorMessage = "Address-2 is required")]
        [MaxLength(100, ErrorMessage = "Address should not exceed 100 characters")]
        public string? Address2 { get; set; }
        [Display(Name = "Address-3")]
        [Required(ErrorMessage = "Address-3 is required")]
        [MaxLength(100, ErrorMessage = "Address should not exceed 100 characters")]
        public string? Address3 { get; set; }
        [Display(Name = "Pincode")]
        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression("^([0-9]{6})$", ErrorMessage = "Invalid Pin Code.")]
        public string? Pincode { get; set; }
        [Display(Name = "Remarks")]
        [Required(ErrorMessage = "Remarks is required")]
        [MaxLength(50, ErrorMessage = "Remarks should not exceed 50 characters")]
        public string? Remarks { get; set; }
        [Display(Name = "Created By")]
        [Required(ErrorMessage = "Created Person Name is required")]
        public int? CreatedBy { get; set; }
        [Display(Name = "Date Of Creation")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Display(Name = "Modified By")]
        public int? ModifiedBy { get; set; }
        [Display(Name = "Modified Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        [JsonIgnore]
        public virtual Tenantmaster? Tenant { get; set; }
        [JsonIgnore]
        public virtual ICollection<Userdetail> Userdetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
