namespace UsfQuiz.Services.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class QuizEvaluationDto
    {
        public int ForQuizId { get; set; }

        [Required]
        public IList<int> SelectedAnswerIds { get; set; }
    }
}
