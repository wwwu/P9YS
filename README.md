# P9YS
影片信息、资源分享  
ASP.NET Core 2.2 + EF Core + LayUI

### 预览
> 生产环境  
  Web：[http://www.p9ys.com](http://www.p9ys.com) （账号：test@p9ys.com 密码：123456）  
  后台：[http://ht.p9ys.com](http://ht.p9ys.com) 从Web端个人中心可进入，共享cookie  
  环境：ubuntu + docker + nginx反向代理
  配置：单核、1G内存、1M带宽
  
> 演示环境：未搭建

### 项目构建
1. 修改P9YS.Web和P9YS.Manage项目下appsettings.*.json中的连接字符串。 **[必须]**
2. 程序包管理器控制台，默认项目设置"P9YS.EntityFramework"，执行命令Update-Database构建数据库。 **[必须]**
3. 如果数据库是MySql8.x，需要手动创建一个空数据库，数据库名与连接字符串HangfireContext中保持一致。
4. 数据库默认字符编码设置为utf8mb4，否则系统抓取到一些数据中含有emoj符号，会导致插入失败。
5. appsettings配置文件中EmailServer（邮件服务），会影响注册邮件的发送。
6. appsettings配置文件中TxCos（腾讯云对象存储，指定额度内免费），会影响图片上传和显示。
7. P9YS.Web项目d的appsettings配置文件中GeetestOptions（极验验证），会影响登录。

### 知识点
> * [ASP.NET Core入门](https://docs.microsoft.com/zh-cn/aspnet/core/getting-started/?view=aspnetcore-2.2&tabs=windows)  
有这种文档简直幸福。
> * [EF Core入门](https://docs.microsoft.com/zh-cn/ef/core/get-started/)  
搭配上ASP.NET Core入门，这两个文档涵盖了项目中所有的知识点。
> * [AutoMapper](https://automapper.readthedocs.io/en/latest/)  
对象的映射工具。
> * [NLog](https://github.com/NLog/NLog.Web)  
一个基于.NET平台编写的日志记录类库。
> * [Hangfire.AspNetCore](https://www.hangfire.io/)  
.NET Core应用程序中执行后台处理的开源框架，无需Windows服务，自带仪表盘。
> * [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)  
EF Core的MySQL提供程序。
> * [Xunit](https://xunit.net/)  
单元测试工具。
> * [Moq](https://github.com/moq/moq4)  
.NET最受欢迎和最友好的模拟框架。
> * [Microsoft.EntityFrameworkCore.InMemory](https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/testing/in-memory)  
内存数据库，配合测试组件使用。

### License
[Apache License 2.0](https://github.com/wwwu/P9YS/blob/master/LICENSE.md)
