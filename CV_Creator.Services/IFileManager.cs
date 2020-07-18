using System.Threading.Tasks;

namespace CV_Creator.Services
{
    public interface IFileManager
    {
        Task SaveToDiskAsync(byte[] pdfCv, string filePath);
    }
}