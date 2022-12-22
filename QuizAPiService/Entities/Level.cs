using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizAPiService.Entities
{
    public partial class Level
    {
        public Level()
        {
            Questiondetails = new HashSet<Questiondetail>();
            Quizdetails = new HashSet<Quizdetail>();
        }

        public int LevelId { get; set; }
        public string LevelType { get; set; } = null!;
        public sbyte IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual ICollection<Questiondetail> Questiondetails { get; set; }
        public virtual ICollection<Quizdetail> Quizdetails { get; set; }
    }
}
