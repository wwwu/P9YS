using System.Threading.Tasks;

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
        Task<Result> UploadFileAsync(string savePath, string sourcePath);

        Task<string> GetClientStringAsync(string url, string encoding = null);

        /// <summary>
        /// 抓取豆瓣html,优先根据url下载，没有url则根据movieName搜索
        /// </summary>
        /// <param name="url"></param>
        /// <param name="movieName"></param>
        /// <returns></returns>
        Task<(string url, string html)> DownloadDoubanHtml(string url, string movieName);

        (string imgName, byte[] dataBytes) Base64ToBytes(string base64String);
    }
}