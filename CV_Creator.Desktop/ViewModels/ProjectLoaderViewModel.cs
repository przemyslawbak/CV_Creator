using CV_Creator.Services;

namespace CV_Creator.Desktop.ViewModels
{
    public interface IProjectLoaderViewModel
    {

    }

    public class ProjectLoaderViewModel : ViewModelBase, IProjectLoaderViewModel, IResultViewModel
    {
        public ProjectLoaderViewModel()
        {

        }

        public object ObjectResult => throw new System.NotImplementedException();
    }
}
