using CV_Creator.Services;

namespace CV_Creator.Desktop.ViewModels
{
    public interface ITechStackLoaderViewModel
    {

    }

    public class TechStackLoaderViewModel : ViewModelBase, ITechStackLoaderViewModel, IResultViewModel
    {
        public object ObjectResult => throw new System.NotImplementedException();
    }
}
