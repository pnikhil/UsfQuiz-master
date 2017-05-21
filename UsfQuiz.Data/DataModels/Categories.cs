namespace UsfQuiz.Data.DataModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CommonModels;
    using Commons;

    public class Categories : BaseModel<int>
    {
        private ICollection<Quiz> quizzes;

        public Categories()
        {
            this.quizzes = new HashSet<Quiz>();
            this.ImageUrl = GlobalConstants.DefaultCategoryImageUrl;
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(GlobalConstants.NameMinLength)]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string Name { get; set; }

        [MinLength(GlobalConstants.UrlMinLength)]
        [MaxLength(GlobalConstants.UrlMaxLength)]
        public string ImageUrl { get; set; }

        public virtual ICollection<Quiz> Quizzes
        {
            get { return this.quizzes; }
            set { this.quizzes = value; }
        }
    }
}
