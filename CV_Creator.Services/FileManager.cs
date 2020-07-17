using System;
using System.IO;

namespace CV_Creator.Services
{
    public class FileManager : IFileManager
    {
        //TODO: async
        public void SaveToDiskAsync(byte[] pdfCv, string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(pdfCv, 0, pdfCv.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
            }
        }
    }
}
