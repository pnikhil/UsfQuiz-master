namespace UsfQuiz.Web.Controllers
{
    using System.Web.Mvc;
    using Data.Commons;
    using Data.DataModels;
    using Services.Interfaces;

    [Authorize]
    public class NewQuizController : BaseController
    {
        private readonly IQuizService _quizzes;

        public NewQuizController(IQuizService quizzes)
        {
            this._quizzes = quizzes;
        }

        [Authorize(Roles = GlobalConstants.Admin)]
        // The Post Method is handled in the api/Quizzes/Create
        [HttpGet]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var quiz = this._quizzes.GetById(id);
            var result = this.CheckQuizPresence(quiz);

            if (result == null)
            {
                this._quizzes.Delete(quiz);
                this.TempData["notification"] = "Quiz successfully deleted";
                result = this.RedirectToAction("Index", "Home");
            }

            return result;
        }

        private ActionResult CheckQuizPresence(Quiz quiz)
        {
            if (quiz == null)
            {
                return this.HttpNotFound("The quiz is missing.");
            }

            bool isAdmin = this.User.IsInRole(GlobalConstants.Admin);

            if (!isAdmin && this.UserId != quiz.CreatedById)
            {
                this.TempData["error"] = "You cannot modify this quiz";
                return this.RedirectToAction("Index", "Home");
            }

            return null;
        }
    }
}
