using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Common
{
    public class AppSettings
    {
        public string MovieResourceContext { get; set; }
        public string DesKey { get; set; }
        public EmailServer EmailServer { get; set; }
        public TxCos TxCos { get; set; }
        public XianLiao XianLiao { get; set; }
    }

    public class EmailServer
    {
        public string SmtpServer { get; set; }
        public string From { get; set; }
        public string FromPwd { get; set; }
    }

    public class TxCos
    {
        public string Appid { get; set; }
        public string SecretID { get; set; }
        public string SecretKey { get; set; }
        public string Bucket { get; set; }
        public string CosDomain { get; set; }
    }

    public class XianLiao
    {
        public string Id { get; set; }
        public string SsoKey { get; set; }
    }
}
