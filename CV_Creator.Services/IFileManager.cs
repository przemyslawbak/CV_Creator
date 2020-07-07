namespace CV_Creator.Services
{
    public interface IFileManager
    {
        void SaveToDiskAsync(byte[] pdfCv, string filePath);
    }
}