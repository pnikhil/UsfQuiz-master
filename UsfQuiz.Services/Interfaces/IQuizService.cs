namespace UsfQuiz.Services.Interfaces
{

    using System.Linq;
    using Data.DataModels;

    public interface IQuizService
    {
        Quiz GetById(int id);

        void Add(Quiz quiz);

        void Save();

        void Delete(Quiz quiz);

        IOrderedQueryable<Quiz> GetQuizzes();
    }
}
