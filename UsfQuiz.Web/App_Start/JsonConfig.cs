namespace UsfQuiz.Web
{
    using System.Web.Http;
    using Newtonsoft.Json.Serialization;

    public static class JsonConfiguration
    {
        public static void UseCamelCase()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
