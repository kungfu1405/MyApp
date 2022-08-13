using Microsoft.Extensions.Configuration;

namespace DbData.Service.Commons
{
    public static class UrlList
    {
        public static IConfiguration Configs { private get; set; }
        public static string IdServer => Configs.GetValue<string>("ApiList:IdServer");

        #region Others API
        public static string FileServer => Configs.GetValue<string>("ApiList:FileServerApi");
        //
        #endregion 
    }
}