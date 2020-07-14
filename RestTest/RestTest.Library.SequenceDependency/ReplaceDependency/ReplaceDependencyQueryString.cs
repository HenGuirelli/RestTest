using RestTest.Library.Entity;
using RestTest.Library.Entity.Http;
using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using RestTest.RestRequest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    internal class ReplaceDependencyQueryString : IReplaceDependency
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, Task<TestResult>> _dict;
        private readonly JsonObjectDependecyReplacer _jsonObjectDependecyReplacer;

        public ReplaceDependencyQueryString(
            DependencyDetector dependencyDetector,
            Dictionary<string, Task<TestResult>> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
            _jsonObjectDependecyReplacer = new JsonObjectDependecyReplacer(_dependencyDetector, _dict);
        }

        public Task Replace(Validation validation)
        {
            var queryString = validation.QueryString;
            return ReplaceDependecy(queryString);
        }

        private Task ReplaceDependecy(JsonObject queryString)
        {
            return _jsonObjectDependecyReplacer.ReplaceDependecy(queryString);
        }

        public Task Replace(RequestConfig requestConfig)
        {
            var queryStringDict = requestConfig.QueryString;
            var queryString = new QueryString(queryStringDict);
            return ReplaceDependecy(queryString);
        }
    }
}
