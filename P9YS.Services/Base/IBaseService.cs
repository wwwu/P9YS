namespace P9YS.Services.Base
{
    public interface IBaseService
    {
        string GetCosAbsoluteUrl(string relativeUrl);
        Result UploadFile(string remotePath, byte[] bytes);
    }
}