using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizMVC.Models
{
    public partial class Questiondetail
    {
        public int QuestionId { get; set; }
        public string QuestionDescription { get; set; } = null!;
        public int CategoryId { get; set; }
        public int LevelId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Type { get; set; }
        public sbyte? IsActive { get; set; }
        public string? CorrectOption { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual CategoryQuestion Category { get; set; } = null!;
        public virtual Level Level { get; set; } = null!;
    }
}