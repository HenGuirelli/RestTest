using RestTest.Library.Entity.Test;
using RestTest.Library.SequenceDependency.ReplaceDependency;
using RestTest.RestRequest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestTest.Library.SequenceDependency
{
    public class SequenceDependencyLocator
    {
        private readonly Dictionary<string, Task<TestResult>> _dict = new Dictionary<string, Task<TestResult>>();
        private readonly DependencyDetector _dependencyDetector = new DependencyDetector();
        private readonly List<IReplaceDependency> _replacers = new List<IReplaceDependency>();

        public SequenceDependencyLocator()
        {
            _replacers.Add(new ReplaceDependencyQueryString(_dependencyDetector, _dict));
            _replacers.Add(new ReplaceDependencyBody(_dependencyDetector, _dict));
            _replacers.Add(new ReplaceDependencyUrl(_dependencyDetector, _dict));
            _replacers.Add(new ReplaceDependencyHeader(_dependencyDetector, _dict));
        }

        public void ReplaceDependency(RequestConfig requestConfig)
        {
            _replacers.ForEach(replacer => replacer.Replace(requestConfig));
        }

        public void ReplaceDependency(Validation validation)
        {
            _replacers.ForEach(replacer => replacer.Replace(validation));
        }

        public void Register(string name, Task<TestResult> operation)
        {
            _dict[name] = operation;
        }
    }
}
