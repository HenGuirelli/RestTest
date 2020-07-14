using RestTest.Configuration;
using RestTest.Library.Entity.Test;
using RestTest.Library.SequenceDependency.ReplaceDependency;
using RestTest.RestRequest;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task ReplaceDependency(RequestConfig requestConfig)
        {
            IEnumerable<Task> tasks = _replacers.Select(replacer => replacer.Replace(requestConfig));
            await Task.WhenAll(tasks);
        }

        public async Task ReplaceDependency(Validation validation)
        {
            IEnumerable<Task> tasks = _replacers.Select(replacer => replacer.Replace(validation));
            await Task.WhenAll(tasks);
        }

        public void Register(string name, Task<TestResult> operation)
        {
            _dict[name] = operation;
        }

        public async Task Wait(UniqueConfiguration item)
        {
            if (string.IsNullOrEmpty(item.Wait)) return;

            if (_dict.TryGetValue(item.Wait, out var testResult))
            {
                await testResult.ConfigureAwait(false);
                return;
            }
            throw new ArgumentException($"\"Wait\": \"{item.Wait}\" not found");
        }
    }
}
