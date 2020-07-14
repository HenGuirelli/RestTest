using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    public class JsonObjectDependecyReplacer
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, Task<TestResult>> _dict;

        public JsonObjectDependecyReplacer(
            DependencyDetector dependencyDetector,
            Dictionary<string, Task<TestResult>> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
        }

        public async Task ReplaceDependecy(JsonObject jsonObject)
        {
            foreach (var item in jsonObject.Keys)
            {
                var jsonItem = jsonObject[item];
                if (jsonItem is JsonObject) await ReplaceDependecy(jsonItem as JsonObject);
                if (jsonItem is JsonString)
                {
                    var value = jsonItem.GetValue().ToString();
                    if (_dependencyDetector.IsDependency(value))
                    {
                        string name = _dependencyDetector.GetDependencyName(value);
                        if (_dict.TryGetValue(name, out var result))
                        {
                            var valueToReplace = _dependencyDetector.Evaluate(value, await result);
                            (jsonItem as JsonString).Value = valueToReplace;
                        }
                    }
                }
            }
        }
    }
}
