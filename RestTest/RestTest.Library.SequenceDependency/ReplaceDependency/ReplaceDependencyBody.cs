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
        private readonly JsonObjectDependecyReplacer _jsonObjectDependecyReplacer;

        public ReplaceDependencyBody(
            DependencyDetector dependencyDetector,
            Dictionary<string, TestResult> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
            _jsonObjectDependecyReplacer = new JsonObjectDependecyReplacer(_dependencyDetector, _dict);
        }

        public void Replace(Validation validation)
        {
            ReplaceDependecy(validation.Body);
        }

        public void Replace(RequestConfig requestConfig)
        {
            var reader = new JsonReader.JsonReaderBody();
            Entity.Http.Body body = reader.Read(requestConfig.Body);
            ReplaceDependecy(body);
        }

        private void ReplaceDependecy(JsonObject body)
        {
            _jsonObjectDependecyReplacer.ReplaceDependecy(body);
        }
    }
}
