using Autofac;
using Rover.Services.Interface;

namespace Rover.Services.Autofac
{
    public class RegisterTypes
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsService>().As<ISettingsService>().InstancePerLifetimeScope();
            builder.RegisterType<RoverNavigationService>().As<IRoverNavigationService>().InstancePerLifetimeScope();
        }
    }
}
