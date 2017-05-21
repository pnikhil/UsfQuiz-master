namespace UsfQuiz.Web.ViewModels.Quiz.Create
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Commons;
    using Services.AutoMappers;

    public class CreateQuizModel : IMapTo<Data.DataModels.Quiz>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.TitleMinLength)]
        [MaxLength(GlobalConstants.TitleMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.DescriptionMinLength)]
        [MaxLength(GlobalConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [MinLength(GlobalConstants.TermsConditionsMinLength)]
        [MaxLength(GlobalConstants.TermsConditionsMaxLength)]
        public string TermsConditions { get; set; }

        public bool IsPrivate { get; set; }

        public bool ShuffleAnswers { get; set; }

        [Range(GlobalConstants.MinQuestionsCount, int.MaxValue)]
        public int NumberOfQuestions { get; set; }

        [Required]
        public CategoryViewModel Category { get; set; }

        [Required]
        public ICollection<QuestionModel> Questions { get; set; }
    }
}

