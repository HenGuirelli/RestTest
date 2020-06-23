using System.Collections.Generic;
using System.Net;

namespace RestTest.HttpServer.Test
{
    internal static class ExtensionMethods
    {
        public static void AddRange(this CookieCollection cookies, IDictionary<string, string> dicitonary)
        {
            foreach(var item in dicitonary)
            {
                cookies.Add(new Cookie(item.Key, item.Value));
            }
        }
    }
}
