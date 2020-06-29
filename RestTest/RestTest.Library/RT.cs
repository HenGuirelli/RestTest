using System.Linq;
using System.Threading.Tasks;
using RestTest.Library.Entity;
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

            var tasksSequence = _config.Sequences.Select(async item =>
            {
                foreach (var sequeceItem in item.Sequence)
                {
                    var request = Requests.Create(sequeceItem.ToRequestConfig());
                    OnTestStart?.Invoke(item.Name);
                    var response = request.Send();
                    var testResult = new TestResult(item.Name, sequeceItem.Validation, await response);
                    OnTestFinished?.Invoke(testResult);
                }
            });

            await Task.WhenAll(tasksUniques);
            await Task.WhenAll(tasksSequence);

            OnAllTestsFinished?.Invoke();
        }
    }
}
