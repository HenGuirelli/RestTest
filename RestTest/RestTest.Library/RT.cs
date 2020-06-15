using System.Threading.Tasks;
using RestTest.RestRequest;

namespace RestTest.Library
{
    public class RT
    {
        private readonly RestTest.Configuration.Configuration _config;
        private readonly RequestCallbackMap _requestCallbackMap;
        private readonly Requests _requests;

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
                var result = request.Send();
                _requestCallbackMap[item.Name]?.OnFinished(new TestResult(result));
            });

            Parallel.ForEach(_config.Sequences, item =>
            {
                foreach (var sequeceItem in item.Sequence)
                {
                    var request = Requests.Create(sequeceItem.ToRequestConfig());
                    _requestCallbackMap[item.Name]?.OnStart();
                    var result = request.Send();
                    _requestCallbackMap[item.Name]?.OnFinished(new TestResult(result));
                }
            });
        }
    }
}
