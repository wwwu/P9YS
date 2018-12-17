using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace P9YS.Common
{
    public enum UserStatusEnum
    {
        [Description("正常")]
        Normal =0,

        [Description("锁定")]
        Locked=1,
    }
}
