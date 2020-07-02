using System.Threading.Tasks;
using System.Windows;

namespace CV_Creator.Services
{
    public interface IWindowManager
    {
        string OpenFileDialogWindow(string filePath);
        Task<object> OpenResultWindow(object viewModel);
    }
}
