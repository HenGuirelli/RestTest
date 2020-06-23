using System.Collections.Generic;
using System.Net;

namespace RestTest.HttpServer.Test
{
    internal static class ExtensionMethods
    {
        public static void AddCookiesRange(this HttpListenerResponse cookies, IDictionary<string, string> dicitonary)
        {
            foreach(var item in dicitonary)
            {
                cookies.SetCookie(new Cookie(item.Key, item.Value));
            }
        }
    }
}
