using System.Threading.Tasks;
using RestTest.RestRequest;

namespace RestTest.Library
{
    public class RT
    {
        private readonly RestTest.Configuration.Configuration _config;
        private readonly RequestCallbackMap _requestCallbackMap;

        public RT(string configPath, RequestCallbackMap requestCallbackMap)
        {
            _config = new RestTest.Configuration.Configuration(configPath);
            _requestCallbackMap = requestCallbackMap;
        }

        public void Start()
        {
            Parallel.ForEach(_config.Uniques, item =>
            {
                var request = Requests.Create(item.ToRequestConfig());
                _requestCallbackMap[item.Name]?.OnStart();
                var response = request.Send();
                var testResult = new TestResult(item.Validation, response);
                _requestCallbackMap[item.Name]?.OnFinished(testResult);
            });

            Parallel.ForEach(_config.Sequences, item =>
            {
                foreach (var sequeceItem in item.Sequence)
                {
                    var request = Requests.Create(sequeceItem.ToRequestConfig());
                    _requestCallbackMap[item.Name]?.OnStart();
                    var response = request.Send();
                    var testResult = new TestResult(sequeceItem.Validation, response);
                    _requestCallbackMap[item.Name]?.OnFinished(testResult);
                }
            });
        }
    }
}
