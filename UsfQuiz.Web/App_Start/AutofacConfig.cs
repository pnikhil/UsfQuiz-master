namespace UsfQuiz.Web
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using AutoMapper;
    using Data;
    using Data.Commons;
    using Data.DataModels;
    using Services.AutoMappers;
    using Services.Interfaces;
    using Controllers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register MVC and WebApi controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            // Register services
            RegisterServices(builder);

            // Register IMapper
            builder.Register(x => MappingBootstrapper.Configuration.CreateMapper()).As<IMapper>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new ApplicationDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            builder.RegisterType<UserStore<User>>()
                .As<IUserStore<User>>();
            builder.RegisterType<UserManager<User>>();

            var servicesAssembly = Assembly.GetAssembly(typeof(IQuizService));
            builder.RegisterAssemblyTypes(servicesAssembly)
                .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(DbRepository<>))
                .As(typeof(IDbRepository<>))
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}
