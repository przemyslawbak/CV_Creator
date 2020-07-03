using Autofac;
using CV_Creator.Desktop.ViewModels;
using CV_Creator.Services;
using System.Windows;

namespace CV_Creator.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IContainer _container;
        private readonly IWindowManager _winService;

        public App()
        {
            _container = CV_Creator.Startup.BootStrap();
            _winService = new WindowManager();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _winService.OpenWindow(_container.Resolve<MainViewModel>());
        }
    }
}
