using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using RestTest.RestRequest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    internal class ReplaceDependencyBody : IReplaceDependency
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, Task<TestResult>> _dict;
        private readonly JsonObjectDependecyReplacer _jsonObjectDependecyReplacer;

        public ReplaceDependencyBody(
            DependencyDetector dependencyDetector,
            Dictionary<string, Task<TestResult>> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
            _jsonObjectDependecyReplacer = new JsonObjectDependecyReplacer(_dependencyDetector, _dict);
        }

        public Task Replace(Validation validation)
        {
            return ReplaceDependecy(validation.Body);
        }

        public Task Replace(RequestConfig requestConfig)
        {
            var reader = new JsonReader.JsonReaderBody();
            Entity.Http.Body body = reader.Read(requestConfig.Body);
            return ReplaceDependecy(body);
        }

        private Task ReplaceDependecy(JsonObject body)
        {
            return _jsonObjectDependecyReplacer.ReplaceDependecy(body);
        }
    }
}
