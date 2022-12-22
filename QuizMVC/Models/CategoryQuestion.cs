using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizMVC.Models
{
    public partial class CategoryQuestion
    {
        public CategoryQuestion()
        {
            Questiondetails = new HashSet<Questiondetail>();
            Quizdetails = new HashSet<Quizdetail>();
        }

        public int CategoryId { get; set; }
        public string CategoryType { get; set; } = null!;
        public sbyte IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual ICollection<Questiondetail> Questiondetails { get; set; }
        public virtual ICollection<Quizdetail> Quizdetails { get; set; }
    }
}