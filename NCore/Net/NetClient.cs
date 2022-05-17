using System.Net;
using System.Web;
using NCore.Extensions;

namespace NCore.Net
{
    public class NetClient
    {
        public UserAgent UserAgent { get; set; } = UserAgent.FIREFIX_77_WINDOWS;

        public string DownloadString(string url) => DownloadString(url, this.UserAgent);

        public string DownloadString(string url, UserAgent userAgent)
        {
            using (WebClient web = new WebClient())
            {
                if(!UserAgent.IsNullOrEmpty(userAgent))
                    web.UserAgent(userAgent);
                var result = web.DownloadString(url);
                return HttpUtility.HtmlDecode(result);
            }
        }
    }
}