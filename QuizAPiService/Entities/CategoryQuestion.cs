﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuizAPiService.Entities
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
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [JsonIgnore]
        public virtual ICollection<Questiondetail> Questiondetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<Quizdetail> Quizdetails { get; set; }
    }
}
