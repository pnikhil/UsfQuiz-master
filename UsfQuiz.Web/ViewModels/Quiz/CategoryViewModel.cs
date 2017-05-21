namespace UsfQuiz.Web.ViewModels.Quiz
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Data.Commons;
    using Data.DataModels;
    using Services.AutoMappers;

    // Todo: Add Constraints
    public class CategoryViewModel : IMapFrom<Categories>, IMapTo<Categories>
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.NameMinLength)]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string Name { get; set; }

        [MinLength(GlobalConstants.UrlMinLength)]
        [MaxLength(GlobalConstants.UrlMaxLength)]
        public string ImageUrl { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
