namespace UsfQuiz.Web
{
    using System.Web.Http;
    using Microsoft.Owin.Security.OAuth;

    public static class WebApiConfiguration
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            //formatters.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
