namespace P9YS.Services.Base
{
    public interface IBaseService
    {
        string GetCosAbsoluteUrl(string relativeUrl);

        /// <summary>
        /// 上传文件到Cos，默认bucket
        /// </summary>
        /// <param name="savePath">要保存的路径 /文件夹名/文件名.jpg</param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        Result UploadFile(string savePath, byte[] bytes);


        /// <summary>
        /// 上传文件到Cos，默认bucket
        /// </summary>
        /// <param name="savePath">要保存的路径, /文件夹名/文件名.jpg</param>
        /// <param name="sourcePath">要上传文件的完整url</param>
        /// <returns></returns>
        Result UploadFile(string savePath, string sourcePath);
    }
}