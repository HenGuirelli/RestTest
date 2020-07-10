using RestTest.Library.Entity;
using RestTest.Library.Entity.Http;
using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using RestTest.RestRequest;
using System.Collections.Generic;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    internal class ReplaceDependencyQueryString : IReplaceDependency
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, TestResult> _dict;
        private readonly JsonObjectDependecyReplacer _jsonObjectDependecyReplacer;

        public ReplaceDependencyQueryString(
            DependencyDetector dependencyDetector,
            Dictionary<string, TestResult> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
            _jsonObjectDependecyReplacer = new JsonObjectDependecyReplacer(_dependencyDetector, _dict);
        }

        public void Replace(Validation validation)
        {
            var queryString = validation.QueryString;
            ReplaceDependecy(queryString);
        }

        private void ReplaceDependecy(JsonObject queryString)
        {
            _jsonObjectDependecyReplacer.ReplaceDependecy(queryString);
        }

        public void Replace(RequestConfig requestConfig)
        {
            var queryStringDict = requestConfig.QueryString;
            var queryString = new QueryString(queryStringDict);
            ReplaceDependecy(queryString);
        }
    }
}
