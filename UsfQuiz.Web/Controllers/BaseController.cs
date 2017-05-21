namespace UsfQuiz.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Services.AutoMappers;

    public abstract class BaseController : Controller
    {
        protected IMapper Mapper
        {
            get
            {
                return MappingBootstrapper.Configuration.CreateMapper();
            }
        }

        protected string UserId
        {
            get { return this.User.Identity.GetUserId(); }
        }

        protected string UserName
        {
            get { return this.User.Identity.GetUserName(); }
        }
    }
}
