using RestTest.Library.Entity.Http;
using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using RestTest.RestRequest;
using System.Collections.Generic;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    internal class ReplaceDependencyHeader : IReplaceDependency
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, TestResult> _dict;
        private readonly JsonObjectDependecyReplacer _jsonObjectDependecyReplacer;

        public ReplaceDependencyHeader(
            DependencyDetector dependencyDetector,
            Dictionary<string, TestResult> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
            _jsonObjectDependecyReplacer = new JsonObjectDependecyReplacer(_dependencyDetector, _dict);
        }

        public void Replace(Validation validation)
        {
            var header = validation.Header;
            ReplaceDependecy(header);
        }

        private void ReplaceDependecy(JsonObject header)
        {
            _jsonObjectDependecyReplacer.ReplaceDependecy(header);
        }

        public void Replace(RequestConfig requestConfig)
        {
            var headerDict = requestConfig.QueryString;
            var header = new Header(headerDict);
            ReplaceDependecy(header);
        }
    }
}
