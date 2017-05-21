using UsfQuiz.Services.Dto;

namespace UsfQuiz.Services.Interfaces
{

    using UsfQuiz.Data.DataModels;
    public interface IQuizEvaluationService
    {
        IEvaluationResult Evaluate(UserAnswers userAnswer);

        IEvaluationResult Evaluate(int solutionId);

        UserAnswers SaveSolution(QuizEvaluationDto quizSolution, string userId);
    }
}
