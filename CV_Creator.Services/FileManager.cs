using System;
using System.IO;
using System.Threading.Tasks;

namespace CV_Creator.Services
{
    public class FileManager : IFileManager
    {
        public async Task SaveToDiskAsync(byte[] pdfCv, string filePath)
        {
            try
            {
                using (FileStream sourceStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    await sourceStream.WriteAsync(pdfCv, 0, pdfCv.Length);
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
            }
        }
    }
}
