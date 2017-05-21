namespace UsfQuiz.Web.ApiControllers
{
    using System;
    using System.Web.Http;
    using Data.Commons;
    using Data.DataModels;
    using Services.Dto;
    using Services.Interfaces;
    using ViewModels.Quiz.Create;

    public class QuizzesController : BaseController
    {
        private readonly IQuizService quizzes;
        private readonly ICategoriesService categories;
        private readonly IQuizEvaluationService quizEvaluationService;

        public QuizzesController(
            IQuizService quizzes,
            ICategoriesService categories,
            IQuizEvaluationService quizEvaluationService)
        {
            this.quizzes = quizzes;
            this.categories = categories;
            this.quizEvaluationService = quizEvaluationService;
        }

        [Authorize(Roles = GlobalConstants.Admin)]
        [HttpPost]
        public IHttpActionResult Create(CreateQuizModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var category = this.categories.GetById(model.Category.Id);
            if (category == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var quiz = this.Mapper.Map<Quiz>(model);
            quiz.Category = category;
            quiz.CreatedById = this.UserId;

            try
            {
                this.quizzes.Add(quiz);
                return this.CreatedAtRoute(
                    "QuizApi", new { action = "Solve", id = quiz.Id }, new { id = quiz.Id });
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Users = "*")]
        public IHttpActionResult Solve(QuizEvaluationDto model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var result = this.quizEvaluationService.SaveSolution(model, this.UserId);
                return this.Ok(this.quizEvaluationService.Evaluate(result));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
