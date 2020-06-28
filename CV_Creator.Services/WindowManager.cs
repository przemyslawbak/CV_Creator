using Microsoft.Win32;
using System.Threading.Tasks;

namespace CV_Creator.Services
{
    public class WindowManager : IWindowManager
    {
        //MSDN
        public string OpenFileDialogWindow(string filePath)
        {
            string result = filePath;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".pdf"; // Default file extension
            saveFileDialog.Filter = "Text documents (.pdf)|*.pdf"; // Filter files by extension
            saveFileDialog.InitialDirectory = filePath;
            saveFileDialog.RestoreDirectory = true;
            bool? dialog = saveFileDialog.ShowDialog();

            // Process save file dialog box results
            if (dialog == true)
            {
                // Save document
                result = saveFileDialog.FileName;
            }

            return result;
        }
    }
}
