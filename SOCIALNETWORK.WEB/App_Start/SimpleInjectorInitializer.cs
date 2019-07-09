[assembly: WebActivator.PostApplicationStartMethod(typeof(SOCIALNETWORK.WEB.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace SOCIALNETWORK.WEB.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using SOCIALNETWORK.WEB.Services.Handlers;
    using SOCIALNETWORK.WEB.Services.Handlers.Implementations;
    using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            container.Register<IApiHandler, ApiHandler>(Lifestyle.Scoped);
            container.Register<ILoginHandler, LoginHandler>(Lifestyle.Scoped);
            container.Register<IStudyCenterHandler, StudyCenterHandler>(Lifestyle.Scoped);
            container.Register<IUserHandler, UserHandler>(Lifestyle.Scoped);
            container.Register<IEventHandler, EventHandler>(Lifestyle.Scoped);
            container.Register<IForumHandler, ForumHanlder>(Lifestyle.Scoped);

        }
    }
}