using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UsfQuiz.Web.Startup))]
namespace UsfQuiz.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
