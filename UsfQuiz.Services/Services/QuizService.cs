namespace UsfQuiz.Services.Services
{
    using System.Linq;
    using Data.Commons;
    using Data.DataModels;
    using Interfaces;

    public class QuizService : IQuizService
    {
        private readonly IDbRepository<Quiz> quizzes;

        public QuizService(
            IDbRepository<Quiz> quizzes)
        {
            this.quizzes = quizzes;
        }

        public Quiz GetById(int id)
        {
            var quiz = this.quizzes.GetById(id);
            return quiz;
        }

        public void Add(Quiz quiz)
        {
            this.quizzes.Add(quiz);
            this.quizzes.Save();
        }

        public void Save()
        {
            this.quizzes.Save();
        }

        public void Delete(Quiz quiz)
        {
            this.quizzes.Delete(quiz);
            this.Save();
        }

        public IOrderedQueryable<Quiz> GetQuizzes()
        {
            var result = this.quizzes.All().OrderByDescending(q => q.Solutions.Count)
                .ThenBy(q => q.CreatedOn);
            return result;
        }
    }
}
