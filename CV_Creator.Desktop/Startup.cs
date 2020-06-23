using Autofac;
using CV_Creator.Desktop.ViewModels;
using CV_Creator.Views;

namespace CV_Creator
{
    public class Startup
    {
        public static IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<InputView>().AsSelf();
            builder.RegisterType<InputViewModel>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
