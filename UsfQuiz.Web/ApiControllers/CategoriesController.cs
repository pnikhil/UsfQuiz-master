namespace UsfQuiz.Web.ApiControllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System;
    using Data.Commons;
    using Data.DataModels;
    using Services.AutoMappers;
    using Services.Interfaces;
    using ViewModels.Quiz;


    //    [Authorize(Roles = GlobalConstants.Admin)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        [AllowAnonymous]
        public IHttpActionResult GetCategories()
        {
            var result = categories.GetAll().To<CategoryViewModel>().ToList();
            return Ok(result);
        }

        public IHttpActionResult GetAll()
        {
            var result = this.categories.GetAll()
                .To<CategoryViewModel>()
                .ToList();

            return this.Ok(result);
        }

        [Authorize(Roles = GlobalConstants.Admin)]
        [HttpPost]
        public IHttpActionResult Edit(CategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var category = this.categories.GetById(model.Id);
            if (category == null)
            {
                return this.NotFound();
            }

            this.Mapper.Map(model, category);
            this.categories.Save();

            return this.Ok();
        }

        [Authorize(Roles = GlobalConstants.Admin)]
        [HttpPost]
        public IHttpActionResult AddNew(CategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var category = this.Mapper.Map<Categories>(model);
                this.categories.Create(category);

                return this.Created($"/api/GetByPattern?pattern={category.Name}", new { id = category.Id });
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UploadImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server
                        .MapPath("~/Content/images/categories/" + postedFile.FileName);

                    // NOTE: To store in memory use postedFile.InputStream
                    postedFile.SaveAs(filePath);
                }

                return this.Created("/Content/images/categories/", "Upload Success");
            }

            return this.BadRequest("Could not upload image");
        }

        [HttpGet]
        public IHttpActionResult GetAvailableImages()
        {
            var directory = new DirectoryInfo(HttpContext.Current.Server
                .MapPath("~/Content/images/categories/"));

            var files = directory.EnumerateFiles()
                .Select(f => f.Name);

            return this.Ok(files);
        }

        [HttpDelete]
        public IHttpActionResult DeleteImage(string name)
        {
            if (!name.StartsWith("/Content/images/categories/"))
            {
                return this.BadRequest();
            }

            var file = new FileInfo(HttpContext.Current.Server
                .MapPath($"~{name}"));

            file.Delete();

            return this.Ok();
        }
    }
}
