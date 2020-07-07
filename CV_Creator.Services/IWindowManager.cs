using System.Threading.Tasks;

namespace CV_Creator.Services
{
    public interface IWindowManager
    {
        string OpenFileDialogWindow(string filePath);
        Task<object> OpenResultWindowAsync(object viewModel);
        void OpenWindow(object vm);
        void CloseWindow(object viewModel);
    }
}
