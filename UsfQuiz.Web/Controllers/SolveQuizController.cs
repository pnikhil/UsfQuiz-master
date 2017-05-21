namespace UsfQuiz.Web.Controllers
{
    using System;
    using System.Web.Mvc.Html;
    using System.Web.Mvc;
    using Services.Dto;
    using Services.Interfaces;
    using System.Linq;
    using ViewModels.Quiz.Solve;

    public class SolveQuizController : BaseController
    {
        private readonly IQuizService _quizzes;
        private readonly IQuizEvaluationService quizEvaluationService;

        public SolveQuizController(IQuizService quizzesService, IQuizEvaluationService quizEvaluationService)
        {
            this._quizzes = quizzesService;
            this.quizEvaluationService = quizEvaluationService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Solve(int id)
        {
            var quiz = this._quizzes.GetById(id);
            if (quiz == null)
            {
                this.TempData["error"] = "Invalid Quiz Id";
                return this.RedirectToRoute("Default");
            }

            var model = this.Mapper.Map<QuizForSolvingModel>(quiz);
            if (quiz.ShuffleAnswers)
            {
                foreach (var question in model.Questions)
                {
                    question.Answers = question.Answers.OrderBy(q => Guid.NewGuid());
                }
            }

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Solve(QuizEvaluationDto solution)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["error"] = "Invalid Solution Data";
                return this.RedirectToAction("Solve");
            }

            try
            {
                var result = this.quizEvaluationService.SaveSolution(solution, this.UserId);
                return this.RedirectToAction("Performance", "Results", new { solutionId = result.Id });
            }
            catch (Exception ex)
            {
                this.TempData["error"] = ex.Message;
                return this.RedirectToAction("Solve");
            }
        }
    }
}
