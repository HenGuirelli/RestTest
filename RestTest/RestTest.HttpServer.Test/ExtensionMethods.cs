using System.Collections.Generic;
using System.Net;

namespace RestTest.HttpServer.Test
{
    internal static class ExtensionMethods
    {
        public static void AddHeaderRange(this HttpListenerResponse header, IDictionary<string, string> dicitonary)
        {
            foreach (var item in dicitonary)
            {
                header.AddHeader(item.Key, item.Value);
            }
        }

        public static void AddCookiesRange(this HttpListenerResponse cookies, IDictionary<string, string> dicitonary)
        {
            foreach(var item in dicitonary)
            {
                cookies.SetCookie(new Cookie(item.Key, item.Value));
            }
        }
    }
}
