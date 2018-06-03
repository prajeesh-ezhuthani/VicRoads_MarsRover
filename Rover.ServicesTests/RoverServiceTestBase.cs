using Autofac;
using Rover.Services;
using Rover.Services.Interface;

namespace Rover.ServicesTests
{
    public class RoverServiceTestBase
    {
        protected ContainerBuilder builder;
        public RoverServiceTestBase()
        {
            builder = new ContainerBuilder();
            builder.RegisterType<RoverNavigationService>().As<IRoverNavigationService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingsService>().As<ISettingsService>().InstancePerLifetimeScope();
        }
    }
    
   
}
