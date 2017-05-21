namespace UsfQuiz.Web.ApiControllers
{
    using System.Web.Http;
    using AutoMapper;
    using Services.AutoMappers;
    using Microsoft.AspNet.Identity;

    public abstract class BaseController : ApiController
    {
        protected string UserId
        {
            get { return this.User.Identity.GetUserId(); }
        }

        protected IMapper Mapper
        {
            get
            {
                return MappingBootstrapper.Configuration.CreateMapper();
            }
        }
    }
}
