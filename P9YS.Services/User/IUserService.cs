using System.Threading.Tasks;
using P9YS.EntityFramework.Models;
using P9YS.Services.User.Dto;

namespace P9YS.Services.User
{
    public interface IUserService
    {
        Task<Result<CurrentUser>> LoginAsync(LoginInput input);
        Task LogoutAsync();
        Task<bool> AccountIsExistAsync(string email);
        Task<Result<bool>> SendVerifyCodeAsync(string email);
        Task<Result<bool>> RegisterAsync(RegisterInput input);
        XianLiaoOutput GetXianLiaoUserInfo();

        /// <summary>
        /// 获取当前用户信息(未登录或过期返回null)
        /// </summary>
        /// <returns></returns>
        CurrentUser GetCurrentUser();
    }
}