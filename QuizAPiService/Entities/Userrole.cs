using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
namespace QuizAPiService.Entities
{
    public partial class Userrole
    {
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int Userid { get; set; }
        public int CompanyId { get; set; }
        public sbyte IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Tenantcompany Company { get; set; } = null!;
        public virtual Roledetail Role { get; set; } = null!;
        public virtual Userdetail User { get; set; } = null!;
    }
}
