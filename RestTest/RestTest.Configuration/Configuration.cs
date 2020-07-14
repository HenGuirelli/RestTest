using Newtonsoft.Json;
using RestTest.Configuration.JsonNotation;
using System;
using System.Collections.Generic;
using System.IO;

namespace RestTest.Configuration
{
    public class Configuration
    {
        private readonly string _filename;
        private readonly List<UniqueConfiguration> _uniques = new List<UniqueConfiguration>();
        public IEnumerable<UniqueConfiguration> Uniques => _uniques.AsReadOnly();

        private readonly HashSet<string> _requestNames = new HashSet<string>();

        public Configuration(string filename)
        {
            _filename = filename;
            VerifyConfigurationFile();
            ReadJSON();
        }

        private void VerifyConfigurationFile()
        {
            if (!File.Exists(_filename)) throw new FileNotFoundException($"configuration file {_filename} not found");
            if (File.ReadAllText(_filename).Replace(" ", "").Replace("\n", "").Replace("\r", "") == "{}") throw new Exception("Json is empty");
        }

        private string AdjustConfigurationFile(string fileContent)
        {
            return fileContent.StartsWith("[") ? fileContent : $"[{fileContent}]";
        }

        private void ReadJSON()
        {
            var fileContent = AdjustConfigurationFile(File.ReadAllText(_filename).Trim());
            var testTypes = JsonConvert.DeserializeObject<List<object>>(fileContent);
            foreach (var testType in testTypes)
            {
                var test = JsonConvert.DeserializeObject<UniqueConfigurationJsonNotation>(testType.ToString());
                VerifyTestNameDuplicated(test);

                var jsonObject = JsonConvert.DeserializeObject<UniqueConfigurationJsonNotation>(testType.ToString());
                var configEntity = JSONToEntityConverter.ConvertUniqueConfiguration(jsonObject);
                _uniques.Add(configEntity);

            }
        }

        private void VerifyTestNameDuplicated(UniqueConfigurationJsonNotation test)
        {
            if (!string.IsNullOrEmpty(test.name) && _requestNames.Contains(test.name))
            {
                throw new Exception($"config duplicated names '{test.name}'");
            }
            _requestNames.Add(test.name);
        }
    }
}
