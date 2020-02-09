using System;

namespace IntelSharp
{
    public class IXApiContext
    {
        public Uri BaseUri { get; set; } = new Uri("https://2.intelx.io");

        /// <summary>Your personal/company's Intelligence X API-key. Defaults to free public key which has limited functionality/access.</summary>
        public string Key { get; set; } = "9df61df0-84f7-4dc7-b34c-8ccfb8646ace";

        public IXApiContext(string baseUrl, string key)
            : this(new Uri(baseUrl), key)
        { }
        public IXApiContext(string baseUrl)
            : this(new Uri(baseUrl))
        { }

        public IXApiContext(Uri baseUri, string key)
            : this(baseUri)
        {
            Key = key;
        }
        public IXApiContext(Uri baseUri)
        {
            BaseUri = baseUri;
        }

        public IXApiContext()
        { }
    }
}
