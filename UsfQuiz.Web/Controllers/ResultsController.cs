namespace UsfQuiz.Web.Controllers
{
    using Services.Interfaces;
    using System.Data.Entity.Core;
    using System.Web.Mvc;

    public class ResultsController : BaseController
    {
        private readonly IQuizEvaluationService _quizEvaluationService;

        public ResultsController(IQuizEvaluationService quizEvaluationService)
        {
            this._quizEvaluationService = quizEvaluationService;
        }

        public ActionResult Performance(int solutionId)
        {
            IEvaluationResult solution;

            try
            {
                solution = this._quizEvaluationService.Evaluate(solutionId);
            }
            catch (ObjectNotFoundException)
            {
                return this.HttpNotFound();
            }

            return this.View(solution);
        }
    }
}