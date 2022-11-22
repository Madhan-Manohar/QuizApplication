using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "CategoryId must be a positive number")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "CategoryType is required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "CategoryType Length should be less than 50 and greaterr than zero")]
        public string CategoryType { get; set; } = null!;

        [Range(0, 1, ErrorMessage = "IsActive must be a number either 1 or 0")]
        [Display(Name = "Active Status")]
        [Required(ErrorMessage = "Active Status is required")]
        public sbyte IsActive { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CreatedBy must be a positive number")]
        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ModifiedBy must be a positive number")]
        [Display(Name = "Modified By")]
        public int? ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        [JsonIgnore]
        public virtual ICollection<Questiondetail> Questiondetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<Quizdetail> Quizdetails { get; set; }
    }
}
