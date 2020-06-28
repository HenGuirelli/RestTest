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
            Task.Run(StartInternal);
        }

        private void StartInternal()
        {
            Parallel.ForEach(_config.Uniques, item =>
            {
                var request = Requests.Create(item.ToRequestConfig());
                OnTestStart?.Invoke(item.Name);
                var response = request.Send();
                var testResult = new TestResult(item.Name, item.Validation, response);
                OnTestFinished?.Invoke(testResult);
            });

            Parallel.ForEach(_config.Sequences, item =>
            {
                foreach (var sequeceItem in item.Sequence)
                {
                    var request = Requests.Create(sequeceItem.ToRequestConfig());
                    OnTestStart?.Invoke(item.Name);
                    var response = request.Send();
                    var testResult = new TestResult(item.Name, sequeceItem.Validation, response);
                    OnTestFinished?.Invoke(testResult);
                }
            });
            OnAllTestsFinished?.Invoke();
        }
    }
}
