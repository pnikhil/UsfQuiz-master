namespace UsfQuiz.Web.ApiControllers
{
    using System.Web.Http;
    using System.Web.Mvc;

    public class ApiRegister : AreaRegistration
    {
        public override string AreaName => "API";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                name: "QuizApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}