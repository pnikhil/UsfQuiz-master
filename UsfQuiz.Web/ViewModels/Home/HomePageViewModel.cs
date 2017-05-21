namespace UsfQuiz.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using Quiz;

    public class HomePageViewModel
    {
        public IEnumerable<QuizBasicViewModel> Quizzes { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}

