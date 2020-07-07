namespace CV_Creator.Services
{
    public interface IEmailManager
    {
        void SendToAddressAsync(byte[] pdfCv, string emailAddress);
    }
}