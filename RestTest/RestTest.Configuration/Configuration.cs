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

        public Configuration(string filename)
        {
            _filename = filename;
            VerifyConfigurationFile();
            ReadJSON();
        }

        private void VerifyConfigurationFile()
        {
            if (!File.Exists(_filename)) throw new FileNotFoundException("configuration file not found");
        }

        private string AdjustConfigurationFile(string fileContent)
        {
            return fileContent.StartsWith("[") ? fileContent : $"[{fileContent}]";
        }

        private void ReadJSON()
        {
            var fileContent = AdjustConfigurationFile(File.ReadAllText(_filename));
            var jsonObjects = JsonConvert.DeserializeObject<List<UniqueConfigurationJsonNotation>>(fileContent);
            foreach (var jsonObject in jsonObjects)
            {
                var configEntity = JSONToEntityConverter.ConvertUniqueConfiguration(jsonObject);
                _uniques.Add(configEntity);
            }
        }
    }
}
