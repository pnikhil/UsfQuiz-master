namespace UsfQuiz.Services.Interfaces
{
    public interface IAvailableAnswer
    {
        int Id { get; }

        string Text { get; }

        bool IsCorrect { get; }
    }
}
