namespace UsfQuiz.Services.Interfaces
{
    using System.Collections.Generic;

    public interface IEvaluationResult
    {
        int ForQuizId { get; }

        string Name { get; }

        ICollection<IQuestionResult> QuestionResults { get; }

        int TotalQuestions { get; }

        int TotalCorrectAnswer { get; }

        double CorrectScorePercent();
    }
}
