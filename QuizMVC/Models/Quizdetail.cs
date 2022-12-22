using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizMVC.Models
{
    public partial class Quizdetail
    {
        public int QuizId { get; set; }
        public int CategoryId { get; set; }
        public int LevelId { get; set; }
        public int Userid { get; set; }
        public int CompanyId { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public string? Status { get; set; }
        public int? TotalScore { get; set; }
        public int? SecureScore { get; set; }
        public sbyte IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        [JsonIgnore]
        public virtual CategoryQuestion? Category { get; set; }// = null!;
        [JsonIgnore]
        public virtual Tenantcompany? Company { get; set; }// = null!;
        [JsonIgnore]
        public virtual Level? Level { get; set; }// = null!;
        [JsonIgnore]
        public virtual Userdetail? User { get; set; }
    }
}