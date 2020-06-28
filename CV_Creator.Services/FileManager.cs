using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CV_Creator.Services
{
    public class FileManager : IFileManager
    {
        public string GetDefaultPdfPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Przemyslaw_Bak_CV.pdf");
        }
    }
}
