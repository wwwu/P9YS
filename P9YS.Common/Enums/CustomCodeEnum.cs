using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace P9YS.Common
{
    /// <summary>
    /// 错误码
    /// </summary>
    public enum CustomCodeEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 10000,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed = 10004,

        /// <summary>
        /// 服务端异常
        /// </summary>
        [Description("服务端异常")]
        Error =10005,

        /// <summary>
        /// 验证码错误
        /// </summary>
        [Description("验证码错误")]
        VerifyCodeError = 11000,

        /// <summary>
        /// 该帐号已被注册
        /// </summary>
        [Description("该帐号已被注册")]
        Registered = 11001,

        /// <summary>
        /// 帐号或密码错误
        /// </summary>
        [Description("帐号或密码错误")]
        PassworError = 11002,

        /// <summary>
        /// 该账户已被锁定
        /// </summary>
        [Description("该账户已被锁定")]
        AccountLocked = 11003,

        /// <summary>
        /// 验证码发送失败
        /// </summary>
        [Description("验证码发送失败")]
        VerifyCodeSendFailed = 11004,

        /// <summary>
        /// 未登录或已过期
        /// </summary>
        [Description("未登录或已过期")]
        Unauthorized = 11005,

        /// <summary>
        /// 已经存在
        /// </summary>
        [Description("已经存在")]
        Repeated = 12000,

        /// <summary>
        /// 状态已变更
        /// </summary>
        [Description("操作失败：状态已变更")]
        StatusChanged = 12001,
    }
}
