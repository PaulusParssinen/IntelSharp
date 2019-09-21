using System;

namespace IntelSharp
{
    public class IXApiContext
    {
        public Uri BaseUri { get; set; }
        public string Key { get; set; }

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
    }
}
