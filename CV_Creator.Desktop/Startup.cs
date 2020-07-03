using Autofac;
using CV_Creator.Desktop.ViewModels;
using CV_Creator.Desktop.Views;
using CV_Creator.Desktop.Views.Controls;
using CV_Creator.Services;

namespace CV_Creator
{
    public class Startup
    {
        public static IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<WindowManager>()
              .As<IWindowManager>().SingleInstance();

            builder.RegisterType<FileManager>()
              .As<IFileManager>().SingleInstance();

            builder.RegisterType<ProjectLoaderViewModel>()
              .As<IProjectLoaderViewModel>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();

            builder.RegisterType<ItUserControl>().AsSelf();
            builder.RegisterType<ItControlViewModel>().AsSelf().SingleInstance();

            builder.RegisterType<OffshoreUserControl>().AsSelf();
            builder.RegisterType<OffshoreControlViewModel>().AsSelf().SingleInstance();

            builder.RegisterType<ProjectLoaderWindow>().AsSelf();
            builder.RegisterType<ProjectLoaderViewModel>().AsSelf();

            return builder.Build();
        }
    }
}
