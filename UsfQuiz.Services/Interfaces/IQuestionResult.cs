namespace UsfQuiz.Services.Interfaces
{
    using System.Collections.Generic;

    public interface IQuestionResult
    {
        string Title { get; }

        int Id { get; }

        int CorrectAnswerId { get; }

        int SelectedAnswerId { get; set; }

        bool AnsweredCorrectly { get; }

        IList<IAvailableAnswer> Answers { get; }
    }
}
