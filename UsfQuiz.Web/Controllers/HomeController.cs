namespace UsfQuiz.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Services.AutoMappers;
    using Services.Interfaces;
    using ViewModels.Home;
    using ViewModels.Quiz;


    public class HomeController : BaseQuizController
    {
      
        private readonly ICategoriesService _categories;

        public HomeController(
            ICategoriesService categories,
            IQuizService ranking)
            : base(ranking)
        {
            this._categories = categories;
        }

        public ActionResult Index()
        {
            var viewModel = new CategoryViewModel()
            {
                Categories = this.GetAllCategories()
            };

            return this.View(viewModel);
        }

        public ActionResult AllCategories()
        {
            var viewModel = new CategoryViewModel()
            {
                Categories = this.GetAllCategories()
            };

            return this.View(viewModel);
        }

        public ActionResult Quizzes(string categoryName)
        {

            var models = this.Ordering.GetQuizzes()
                .Where(q => q.Category.Name == categoryName)
                .To<QuizBasicViewModel>()
                .ToArray();

            var viewModel = new HomePageViewModel()
            {
                Quizzes = models,
                Categories = this.GetAllCategories(),
            };

            return this.View(viewModel);
        }

        private IList<CategoryViewModel> GetAllCategories()
        {
            var categories =
                this._categories.GetAll()
                    .To<CategoryViewModel>()
                    .ToList();

            return categories;
        }
    }
}