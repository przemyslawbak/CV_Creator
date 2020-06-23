using Autofac;
using CV_Creator.Desktop.ViewModels;

namespace CV_Creator.Desktop.ViewModels
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            _container = Startup.BootStrap();
        }

        public InputViewModel InputViewModel
        {
            get
            {
                return _container.Resolve<InputViewModel>();
            }
        }
    }
}
