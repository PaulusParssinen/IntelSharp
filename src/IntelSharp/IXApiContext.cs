using System;

namespace IntelSharp
{
    public class IXApiContext
    {
        public Uri BaseUri { get; set; } = new Uri("https://2.intelx.io");

        /// <summary>
        /// Your Intelligence X user API key.
        /// </summary>
        public string Key { get; set; }

        public IXApiContext(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "You must specify an API key in order to use Intelligence X API functions.");

            Key = key;
        }
        public IXApiContext()
        { }
    }
}
