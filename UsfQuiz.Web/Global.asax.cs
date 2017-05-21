using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UsfQuiz.Data;
using UsfQuiz.Data.Migrations;
using UsfQuiz.Services.AutoMappers;
using Configuration = UsfQuiz.Data.Migrations.Configuration;

namespace UsfQuiz.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            AutofacConfig.RegisterAutofac();
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var config = new HttpConfiguration();
            JsonConfiguration.UseCamelCase();
            WebApiConfiguration.Register(config);
            MappingBootstrapper.Init();
        }
    }
}
