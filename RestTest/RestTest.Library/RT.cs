using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var request = Requests.Create(item);
                _requestCallbackMap[item.Name]?.OnStart();
                var result = request.Send();
                _requestCallbackMap[item.Name]?.OnFinished(result);
            });

            foreach(var item in _config.Sequences)
            {
                foreach(var sequeceItem in item.Sequence)
                {
                    var request = Requests.Create(sequeceItem);
                    _requestCallbackMap[item.Name]?.OnStart();
                    var result = request.Send();
                    _requestCallbackMap[item.Name]?.OnFinished(result);
                }
            }
        }
    }
}
