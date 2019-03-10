using System.Threading.Tasks;
using P9YS.EntityFramework.Models;
using P9YS.Services.User.Dto;

namespace P9YS.Services.User
{
    public interface IUserService
    {
        Task<Result<CurrentUser>> Login(LoginInput input);
        Task Logout();
        Task<bool> AccountIsExist(string email);
        Task<Result<bool>> SendVerifyCode(string email);
        Task<Result<bool>> Register(RegisterInput input);
        XianLiaoOutput GetXianLiaoUserInfo();

        /// <summary>
        /// 获取当前用户信息(未登录或过期返回null)
        /// </summary>
        /// <returns></returns>
        CurrentUser GetCurrentUser();

        Task<PagingOutput<UserManageOutput>> GetUsers(PagingInput<UserManage_Search_Input> pagingInput);
    }
}