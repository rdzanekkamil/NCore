using System.Net;
using NCore.Net;

namespace NCore.Extensions
{
    public static class WebClientExtensions
    {
        public static void UserAgent(this WebClient o, string userAgent) => o.Headers.Add("user-agent", userAgent);

        public static void UserAgent(this WebClient o, UserAgent userAgent) => o.Headers.Add("user-agent", userAgent);
    }
}