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

        internal void ReadJSON()
        {
            var fileContent = File.ReadAllText(_filename);
            if (fileContent.StartsWith("["))
            {
                var jsonObjects = JsonConvert.DeserializeObject<List<UniqueConfigurationJsonNotation>>(fileContent);
                foreach (var jsonObject in jsonObjects) 
                {
                    var configEntity = JSONToEntityConverter.ConvertUniqueConfiguration(jsonObject);
                    _uniques.Add(configEntity);
                }
            }
            else
            {
                var jsonObject = JsonConvert.DeserializeObject<UniqueConfigurationJsonNotation>(fileContent);
                var configEntity = JSONToEntityConverter.ConvertUniqueConfiguration(jsonObject);
                _uniques.Add(configEntity);
            }
        }
    }
}
