using RestTest.Library.Entity;
using RestTest.RestRequest;
using System.Collections.Generic;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    internal class ReplaceDependencyQueryString : IReplaceDependency
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, TestResult> _dict;

        public ReplaceDependencyQueryString(
            DependencyDetector dependencyDetector,
            Dictionary<string, TestResult> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
        }

        public void Replace(Validation validation)
        {
            var queryString = validation.QueryString;
            ReplaceDependecy(queryString);
        }

        public void Replace(RequestConfig requestConfig)
        {
            var queryString = requestConfig.QueryString;
            ReplaceDependecy(queryString);
        }

        private void ReplaceDependecy(IDictionary<string, string> queryString)
        {
            foreach (var item in queryString)
            {
                if (_dependencyDetector.IsDependency(item.Value))
                {
                    string name = _dependencyDetector.GetDependencyName(item.Value);
                    if (_dict.TryGetValue(name, out var result))
                    {
                        var valueToReplace = _dependencyDetector.Evaluate(item.Value, result);
                        queryString[item.Key] = valueToReplace;
                    }
                }
            }
        }
    }
}
