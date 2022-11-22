﻿using System;
using System.Collections.Generic;

namespace QuizAPiService.Entities
{
    public partial class Tenantmaster
    {
        public Tenantmaster()
        {
            Tenantcompanies = new HashSet<Tenantcompany>();
        }

        public int TenantId { get; set; }
        public string TenantName { get; set; } = null!;
        public string? Pannumber { get; set; }
        public string? Tannumber { get; set; }
        public string? Pfreg { get; set; }
        public string? Esireg { get; set; }
        public sbyte IsActive { get; set; }
        public string? CompanyLo { get; set; }
        public int? PasswordExpiry { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? Pincode { get; set; }
        public string? Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Tenantcompany> Tenantcompanies { get; set; }
    }
}
