using System;
using System.Collections.Generic;
using System.Text;

public static class StringExt
{
    /// <summary>
    /// 隐藏Email前缀 su***@p9ys.com
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static string HideEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return email;
        var sp = email.Split('@');
        sp[0] = sp[0].Length < 3 ? sp[0].Substring(0, 1) : sp[0].Substring(0, 2);
        return $"{sp[0]}***@{sp[1]}";
    }
}
