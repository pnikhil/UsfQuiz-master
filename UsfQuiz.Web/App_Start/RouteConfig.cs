﻿namespace UsfQuiz.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SubmitSolution",
                url: "Quizzes/Solve",
                defaults: new { controller = "SolveQuiz", action = "Solve" },
                namespaces: new[] { "UsfQuiz.Web.Controllers" });

            routes.MapRoute(
                name: "SolveQuiz",
                url: "Quizzes/Solve/{id}",
                defaults: new { controller = "SolveQuiz", action = "Solve" },
                namespaces: new[] { "UsfQuiz.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "UsfQuiz.Web.Controllers" });
        }
    }
}
