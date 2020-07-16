using Autofac;
using CV_Creator.DAL;
using CV_Creator.Desktop.ViewModels;
using CV_Creator.Desktop.Views;
using CV_Creator.Desktop.Views.Controls;
using CV_Creator.Services;
using Microsoft.EntityFrameworkCore;
namespace CV_Creator
{
    public class Startup
    {
        public static IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ProjectRepository>()
                .As<IProjectRepository>();

            builder.RegisterType<TechRepository>()
                .As<ITechRepository>();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ProjectsDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Portfolio_Strona;Trusted_Connection=True;MultipleActiveResultSets=true"); //TODO: from config

            builder.RegisterType<ProjectsDbContext>()
            .WithParameter("options", dbContextOptionsBuilder.Options)
            .InstancePerLifetimeScope();


            builder.RegisterType<WindowManager>()
              .As<IWindowManager>().SingleInstance(); //singleton for window collection prop

            builder.RegisterType<ProjectCollectionDisplayService>()
              .As<IProjectCollectionDisplayService>();

            builder.RegisterType<EmailManager>()
              .As<IEmailManager>();

            builder.RegisterType<DataProcessor>()
              .As<IDataProcessor>();

            builder.RegisterType<FileManager>()
              .As<IFileManager>();

            builder.RegisterType<ProjectLoaderViewModel>()
              .As<IProjectLoaderViewModel>();

            builder.RegisterType<TechStackLoaderViewModel>()
              .As<ITechStackLoaderViewModel>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();

            builder.RegisterType<ItUserControl>().AsSelf();
            builder.RegisterType<ItControlViewModel>().AsSelf().SingleInstance();

            builder.RegisterType<OffshoreUserControl>().AsSelf();
            builder.RegisterType<OffshoreControlViewModel>().AsSelf().SingleInstance();

            builder.RegisterType<ProjectLoaderWindow>().AsSelf();

            builder.RegisterType<TechStackLoaderWindow>().AsSelf();

            return builder.Build();
        }
    }
}
