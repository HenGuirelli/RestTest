using RestTest.Library.Entity;
using RestTest.NewJsonHelper;
using RestTest.RestRequest;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RestTest.Library.SequenceDependency
{
    public class SequenceDependencyLocator
    {
        private readonly Dictionary<string, TestResult> _dict = new Dictionary<string, TestResult>();
        private readonly DependencyDetector _dependencyDetector = new DependencyDetector();

        public void ReplaceDependency(RequestConfig requestConfig)
        {
            requestConfig.QueryString = ReplaceDependecyQueryString(requestConfig.QueryString);
            var reader = new JsonReader.JsonReader();
            var body = reader.Read(requestConfig.Body);
            ReplaceDependecyBody(body);
        }

        private void ReplaceDependecyBody(JsonObject body)
        {
            foreach (var item in body.Keys)
            {
                var bodyItem = body[item];
                if (bodyItem is JsonObject) ReplaceDependecyBody(bodyItem as JsonObject);
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

        private Dictionary<string, string> ReplaceDependecyQueryString(IDictionary<string, string> queryString)
        {
            var r = new Dictionary<string, string>();
            foreach (var item in queryString)
            {
                if (_dependencyDetector.IsDependency(item.Value))
                {
                    string name = _dependencyDetector.GetDependencyName(item.Value);
                    if (_dict.TryGetValue(name, out var result))
                    {
                        var valueToReplace = _dependencyDetector.Evaluate(item.Value, result);
                        r[item.Key] = valueToReplace;
                    }
                }
                else
                {
                    r[item.Key] = item.Value;
                }
            }
            return r;
        }

        public void Register(TestResult testResult)
        {
            _dict[testResult.TestName] = testResult;
        }
    }
}
