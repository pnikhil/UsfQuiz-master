namespace UsfQuiz.Web.Controllers
{
    using Data.Commons;
    using Services.Interfaces;
    using System.Web.Mvc;

    [Authorize(Roles = GlobalConstants.Admin)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService _categories;

        public CategoriesController(ICategoriesService categories)
        {
            this._categories = categories;
        }

        [HttpGet]
        public ActionResult Manage()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult GetEditorTemplate()
        {
            return this.PartialView("_EditPartial");
        }

    }
}