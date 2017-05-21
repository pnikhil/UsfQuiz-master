namespace UsfQuiz.Web.Controllers
{
    using Services.Interfaces;

    public abstract class BaseQuizController : BaseController
    {
        protected BaseQuizController(IQuizService ordering)
        {
            this.Ordering = ordering;
        }

        protected IQuizService Ordering { get; }
    }
}
