using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace QuizAPiService.Entities
{
    public partial class Questiondetail
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "QuestionId must be a positive number")]
        [Display(Name = "Question Id")]
        public int QuestionId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Question Description is required")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Question Description Length should be less than 200 and greater than zero")]

        [Display(Name = "Question Description")]
        public string QuestionDescription { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "CategoryId must be a positive number")]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Level Id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "LevelId must be a positive number")]
        [Display(Name = "Level Id")]
        public int LevelId { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Url should be Valid")]
        [Display(Name = "Image Url")]
        
        public string? ImageUrl { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Question Type Should Be Mandatory")]
        [Display(Name = "Question Type")]
        [RegularExpression(@"^(?:easy|Easy|medium|Medium|Advanced|advanced)$", ErrorMessage = "Invalid Question Type (Enter Only Easy/Medium/Advanced )")]
        public string? Type { get; set; }

        [Range(0, 1, ErrorMessage = "IsActive must be a number either 1 or 0 number")]
        [Display(Name = "Is Active")]
        [RegularExpression("([0-1]+)")]
        public sbyte? IsActive { get; set; }

        
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Correct Option Length should be less than 100 and greater than zero")]
        [Display(Name = "Correct Option")]
        public string? CorrectOption { get; set; }

        
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Option A Length should be less than 100 and greater than zero")]
        [Display(Name = "Option A")]
        public string? OptionA { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Option B Length should be less than 100 and greater than zero")]
        [Display(Name = "Option B")]
        public string? OptionB { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Option C Length should be less than 100 and greater than zero")]
        [Display(Name = "Option C")]
        public string? OptionC { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Option D Length should be less than 100 and greater than zero")]
        [Display(Name = "Option D")]
        public string? OptionD { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "CreatedBy must be a positive number")]
        [Display(Name = "Created By")]
        [RegularExpression("([0-9]+)")]
        public int? CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [RegularExpression("([0-9]+)")]
        [Display(Name = "Modified By")]
        public int? ModifiedBy { get; set; }

     
        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Range(0, 1, ErrorMessage = "Is Random Selected must be a number either 1 or 0 number")]
        [Display(Name = "Is Random Selected")]
        [RegularExpression("([0-1]+)")]
        public sbyte? IsRandomSelected { get; set; }

      
        [JsonIgnore]
        public virtual CategoryQuestion? Category { get; set; } = null!;
        [JsonIgnore]
        public virtual Level? Level { get; set; } = null!;
    }
}
