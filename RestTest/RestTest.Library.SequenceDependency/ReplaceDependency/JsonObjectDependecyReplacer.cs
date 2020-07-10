using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using System.Collections.Generic;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    public class JsonObjectDependecyReplacer
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, TestResult> _dict;

        public JsonObjectDependecyReplacer(
            DependencyDetector dependencyDetector,
            Dictionary<string, TestResult> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
        }

        public void ReplaceDependecy(JsonObject jsonObject)
        {
            foreach (var item in jsonObject.Keys)
            {
                var jsonItem = jsonObject[item];
                if (jsonItem is JsonObject) ReplaceDependecy(jsonItem as JsonObject);
                if (jsonItem is JsonString)
                {
                    var value = jsonItem.GetValue().ToString();
                    if (_dependencyDetector.IsDependency(value))
                    {
                        string name = _dependencyDetector.GetDependencyName(value);
                        if (_dict.TryGetValue(name, out var result))
                        {
                            var valueToReplace = _dependencyDetector.Evaluate(value, result);
                            (jsonItem as JsonString).Value = valueToReplace;
                        }
                    }
                }
            }
        }
    }
}
