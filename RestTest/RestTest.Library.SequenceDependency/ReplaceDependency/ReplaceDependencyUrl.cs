using RestTest.Library.Entity;
using RestTest.Library.Entity.Test;
using RestTest.RestRequest;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    internal class ReplaceDependencyUrl : IReplaceDependency
    {
        private readonly DependencyDetector _dependencyDetector;
        private readonly Dictionary<string, TestResult> _dict;

        public ReplaceDependencyUrl(
            DependencyDetector dependencyDetector,
            Dictionary<string, TestResult> dict)
        {
            _dependencyDetector = dependencyDetector;
            _dict = dict;
        }

        public void Replace(Validation validation)
        {
            // Nothing to seee here
        }

        public void Replace(RequestConfig requestConfig)
        {
            if (_dependencyDetector.IsDependency(requestConfig.Url))
            {
                string name = _dependencyDetector.GetDependencyName(requestConfig.Url);
                if (_dict.TryGetValue(name, out var result))
                {
                    var valueToReplace = _dependencyDetector.Evaluate(requestConfig.Url, result);
                    requestConfig.Url = Regex.Replace(requestConfig.Url, @"\$\{.*\}", valueToReplace);
                }
            }
        }
    }
}
