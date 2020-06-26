using Autofac;

namespace CV_Creator.Desktop.ViewModels
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            _container = Startup.BootStrap();
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return _container.Resolve<MainViewModel>();
            }
        }

        public OffshoreControlViewModel OffshoreControlViewModel
        {
            get
            {
                return _container.Resolve<OffshoreControlViewModel>();
            }
        }

        public ItControlViewModel ItControlViewModel
        {
            get
            {
                return _container.Resolve<ItControlViewModel>();
            }
        }
    }
}
