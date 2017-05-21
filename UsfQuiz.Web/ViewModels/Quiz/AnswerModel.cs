namespace UsfQuiz.Web.ViewModels.Quiz
{
    using System.ComponentModel.DataAnnotations;
    using Data.Commons;
    using Data.DataModels;
    using Services.AutoMappers;

    public class AnswerModel : IMapFrom<Solutions>, IMapTo<Solutions>
    {
        public int Id { get; set; }

        [MinLength(GlobalConstants.AnswerMinLength)]
        [MaxLength(GlobalConstants.AnswerMaxLength)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
