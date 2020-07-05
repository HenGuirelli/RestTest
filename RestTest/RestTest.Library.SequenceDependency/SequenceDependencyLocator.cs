using RestTest.Library.Entity;
using RestTest.Library.Entity.Test;
using RestTest.Library.SequenceDependency.ReplaceDependency;
using RestTest.RestRequest;
using System.Collections.Generic;

namespace RestTest.Library.SequenceDependency
{
    public class SequenceDependencyLocator
    {
        private readonly Dictionary<string, TestResult> _dict = new Dictionary<string, TestResult>();
        private readonly DependencyDetector _dependencyDetector = new DependencyDetector();
        private readonly List<IReplaceDependency> _replacers = new List<IReplaceDependency>();

        public SequenceDependencyLocator()
        {
            _replacers.Add(new ReplaceDependencyQueryString(_dependencyDetector, _dict));
            _replacers.Add(new ReplaceDependencyBody(_dependencyDetector, _dict));
            _replacers.Add(new ReplaceDependencyUrl(_dependencyDetector, _dict));
        }

        public void ReplaceDependency(RequestConfig requestConfig)
        {
            _replacers.ForEach(replacer => replacer.Replace(requestConfig));
        }

        public void Register(TestResult testResult)
        {
            _dict[testResult.TestName] = testResult;
        }

        public void ReplaceDependency(Validation validation)
        {
            _replacers.ForEach(replacer => replacer.Replace(validation));
        }
    }
}
