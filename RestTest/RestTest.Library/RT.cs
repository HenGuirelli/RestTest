using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestTest.Library.Entity;
using RestTest.Library.SequenceDependency;
using RestTest.RestRequest;

namespace RestTest.Library
{
    public class RT
    {
        private readonly RestTest.Configuration.Configuration _config;
        public TestFinishedHandle OnTestFinished;
        public TestStartHandle OnTestStart;
        public TestAllTestFinishedHandle OnAllTestsFinished;

        public RT(string configPath)
        {
            _config = new RestTest.Configuration.Configuration(configPath);
        }

        public void Start()
        {
            StartInternal();
        }

        private async void StartInternal()
        {
            var tasksUniques = _config.Uniques.Select(async item =>
            {
                var request = Requests.Create(item.ToRequestConfig());
                OnTestStart?.Invoke(item.Name);
                var response = request.Send();
                var testResult = new TestResult(item.Name, item.Validation, await response);
                OnTestFinished?.Invoke(testResult);
            });

            foreach (var item in _config.Sequences)
            {
                var sequenceDependency = new SequenceDependencyResolver();
                foreach (var sequeceItem in item.Sequence)
                {
                    var requestConfig = sequeceItem.ToRequestConfig();
                    sequenceDependency.ReplaceDependency(requestConfig);
                    var request = Requests.Create(requestConfig);
                    OnTestStart?.Invoke(sequeceItem.Name);
                    var response = request.Send();
                    var testResult = new TestResult(sequeceItem.Name, sequeceItem.Validation, await response);
                    sequenceDependency.AddResult(testResult);
                    OnTestFinished?.Invoke(testResult);
                }
            };

            await Task.WhenAll(tasksUniques);

            OnAllTestsFinished?.Invoke();
        }
    }
}
