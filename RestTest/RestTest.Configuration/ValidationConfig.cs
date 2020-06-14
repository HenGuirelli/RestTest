using System.Collections.Generic;

namespace RestTest.Library.Config
{
    public class ValidationConfig
    {
        public List<KeyValuePair<string, string>> Body { get; private set; }
        public List<KeyValuePair<string, string>> Header { get; set; }
        public List<KeyValuePair<string, string>> QueryString { get; private set; }
        public List<KeyValuePair<string, string>> Cookies { get; private set; }
        public int Status { get; private set; }

        public int MaxTime { get; private set; }
        public int MinTime { get; set; }
    }
}
