using System.Text.RegularExpressions;

namespace NCore.Net
{
    public class WebUrl : Uri
    {
        public string DomainName => ExtractDomainName(this.AbsoluteUri);
        public string DomainNameClear => DomainName.Replace('.', '_');
        public string UriString { get; }
        public WebUrl(string uriString) : base(uriString)
        {
            this.UriString = uriString;
        }

        public static implicit operator string(WebUrl o) => o.AbsoluteUri;
        public static implicit operator WebUrl(string o) => new WebUrl(o);
        
        public Uri ToUri() => (Uri)this;

        public static string ExtractDomainName(string Url) 
            => Regex.Replace(Url, @"^([a-zA-Z]+:\/\/)?([^\/]+)\/.*?$", "$2");
    }
}