using System.Collections.Generic;

namespace RestTest.Library.Config
{
    public class UniqueTestConfig : TestConfig
    {
        public string url { get; set; }
        public ValidationConfig validation { get; set; }
        public Dictionary<string, object> header { get; set; }
        public Dictionary<string, object> cookie { get; set; }
        public Dictionary<string, object> query_string { get; set; }
        public Method method { get; set; }
    }
}
