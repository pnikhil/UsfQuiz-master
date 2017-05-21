namespace UsfQuiz.Web.ViewModels.Quiz
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Commons;
    using Data.DataModels;
    using Services.AutoMappers;

    public class QuestionModel : IMapTo<Questions>, IMapFrom<Questions>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.QuestionMinLength)]
        [MaxLength(GlobalConstants.QuestionMaxLength)]
        public string Title { get; set; }

        public virtual IEnumerable<AnswerModel> Answers { get; set; }
    }
}
