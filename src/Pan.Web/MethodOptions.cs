using System;
using System.Net.Http;

namespace Pan.Web
{
    /// <summary>
    /// </summary>
    public class MethodOptions
    {
        /// <summary>
        /// </summary>
        public Func<HttpClient, HttpClient> Client { get; set; }

        /// <summary>
        /// </summary>
        public Func<object, string> Serializer { get; set; }

        /// <summary>
        /// </summary>
        public Func<string, object> Deserializer { get; set; }
    }
}