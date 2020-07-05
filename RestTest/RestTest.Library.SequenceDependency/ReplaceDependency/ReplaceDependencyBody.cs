using RestTest.Library.Entity;
using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using RestTest.RestRequest;
using System.Collections.Generic;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    internal class ReplaceDependencyBody : IReplaceDependency
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, TestResult> _dict;

        public ReplaceDependencyBody(
            DependencyDetector dependencyDetector,
            Dictionary<string, TestResult> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
        }

        public void Replace(Validation validation)
        {
            ReplaceDependecy(validation.Body);
        }

        public void Replace(RequestConfig requestConfig)
        {
            var reader = new JsonReader.JsonReader();
            Entity.Http.Body body = reader.Read(requestConfig.Body);
            ReplaceDependecy(body);
        }

        private void ReplaceDependecy(JsonObject body)
        {
            foreach (var item in body.Keys)
            {
                var bodyItem = body[item];
                if (bodyItem is JsonObject) ReplaceDependecy(bodyItem as JsonObject);
                if (bodyItem is JsonString)
                {
                    var value = bodyItem.GetValue().ToString();
                    if (_dependencyDetector.IsDependency(value))
                    {
                        string name = _dependencyDetector.GetDependencyName(value);
                        if (_dict.TryGetValue(name, out var result))
                        {
                            var valueToReplace = _dependencyDetector.Evaluate(value, result);
                            (bodyItem as JsonString).Value = valueToReplace;
                        }
                    }
                }
            }
        }
    }
}
