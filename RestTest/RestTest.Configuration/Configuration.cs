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

        private readonly List<SequenceConfiguration> _sequence = new List<SequenceConfiguration>();
        public IEnumerable<SequenceConfiguration> Sequences => _sequence.AsReadOnly();

        private readonly HashSet<string> _requestNames = new HashSet<string>();
        private readonly List<string> _testTypes = new List<string>(Enum.GetNames(typeof(TestType)));

        public Configuration(string filename)
        {
            _filename = filename;
            VerifyConfigurationFile();
            ReadJSON();
        }

        private void VerifyConfigurationFile()
        {
            if (!File.Exists(_filename)) throw new FileNotFoundException("configuration file not found");
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
            foreach(var testType in testTypes)
            {
                var test = JsonConvert.DeserializeObject<UniqueConfigurationJsonNotation>(testType.ToString());
                VerifyTestNameDuplicated(test);
                VerifyTestType(test);

                if (test.type == "unique_test")
                {
                    var jsonObject = JsonConvert.DeserializeObject<UniqueConfigurationJsonNotation>(testType.ToString());
                    var configEntity = JSONToEntityConverter.ConvertUniqueConfiguration(jsonObject);
                    _uniques.Add(configEntity);
                }
                else
                {
                    var jsonObject = JsonConvert.DeserializeObject<SequenceConfigurationJsonNotation>(testType.ToString());
                    var configEntity = JSONToEntityConverter.ConvertSequenceConfiguration(jsonObject);
                    _sequence.Add(configEntity);
                }
            }
        }

        private void VerifyTestType(UniqueConfigurationJsonNotation test)
        {
            if (!_testTypes.Contains(test.type))
            {
                throw new Exception($"Test '{test.name}' need a type. Example: \"type\": \"unique_test\"");
            }
        }

        private void VerifyTestNameDuplicated(UniqueConfigurationJsonNotation test)
        {
            if (_requestNames.Contains(test.name))
            {
                throw new Exception($"config duplicated names '{test.name}'");
            }
            _requestNames.Add(test.name);
        }
    }
}
