using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RestTest.Library.Config
{
    public class TestConfigReader : IEnumerable<UniqueTestConfig>
    {
        public List<UniqueTestConfig> Tests = new List<UniqueTestConfig>();

        public void ReadFromFile(string path)
        {
            ReadFromString(File.ReadAllText(path));
        }

        public void ReadFromString(string json)
        {
            Tests = JsonConvert.DeserializeObject<List<UniqueTestConfig>>(json);
        }

        public IEnumerator<UniqueTestConfig> GetEnumerator()
        {
            return Tests.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Tests.GetEnumerator();
        }
    }
}
