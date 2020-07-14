using RestTest.Library.Entity.Http;
using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using RestTest.RestRequest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    internal class ReplaceDependencyHeader : IReplaceDependency
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, Task<TestResult>> _dict;
        private readonly JsonObjectDependecyReplacer _jsonObjectDependecyReplacer;

        public ReplaceDependencyHeader(
            DependencyDetector dependencyDetector,
            Dictionary<string, Task<TestResult>> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
            _jsonObjectDependecyReplacer = new JsonObjectDependecyReplacer(_dependencyDetector, _dict);
        }

        public Task Replace(Validation validation)
        {
            var header = validation.Header;
            return ReplaceDependecy(header);
        }

        private Task ReplaceDependecy(JsonObject header)
        {
            return _jsonObjectDependecyReplacer.ReplaceDependecy(header);
        }

        public Task Replace(RequestConfig requestConfig)
        {
            var headerDict = requestConfig.QueryString;
            var header = new Header(headerDict);
            return ReplaceDependecy(header);
        }
    }
}
