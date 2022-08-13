using Mic.Core.Security;
using Microsoft.Extensions.Configuration;

namespace Mic.Core.Website
{
    public static class UrlList
    {
        public static IConfiguration Configs { private get; set; }
        public static string IdServer => Configs.GetValue<string>("ApiList:IdServer");
        public static string IdsClientId => Configs.GetValue<string>("ApiList:IdsClientId");
        public static string IdsClientSecret
        {
            get
            {
                var secret = Configs.GetValue<string>("ApiList:IdsClientSecret");
                if (string.IsNullOrWhiteSpace(secret))
                    return "";

                return new AES().Decode(secret);                
            }
        }
        public static string FileServer => Configs.GetValue<string>("ApiList:FileServer");
        public static string DbDataService => Configs.GetValue<string>("ApiList:DbDataService");
        public static string DynamicDataService => Configs.GetValue<string>("ApiList:DynamicDataService");

        #region Others API

        #endregion 

        public static string FileUrl(string uri)
        {
            var serverPath = FileServer;
            if (FileServer.EndsWith("/"))
                serverPath = FileServer.Substring(0, FileServer.Length - 2);
            if (uri.StartsWith("/"))
                uri = uri.Substring(1);
            return FileServer + "/" + uri;
        }
    }
}