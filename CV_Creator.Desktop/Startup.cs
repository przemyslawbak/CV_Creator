using Autofac;
using CV_Creator.Desktop.ViewModels;
using CV_Creator.Desktop.Views;
using CV_Creator.Desktop.Views.Controls;

namespace CV_Creator
{
    public class Startup
    {
        public static IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<ItUserControl>().AsSelf();
            builder.RegisterType<ItControlViewModel>().AsSelf().SingleInstance();

            builder.RegisterType<OffshoreUserControl>().AsSelf();
            builder.RegisterType<OffshoreControlViewModel>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
