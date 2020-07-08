using RestTest.RestRequest;
using System.Collections.Concurrent;
using RestTestResult = RestTest.Library.Entity.Test.TestResult;

namespace RestTest.Library.Test
{
    internal class ClassTest
    {
        public Requests Request { get; set; }
        public ConcurrentDictionary<string, RestTestResult> Results { get; private set; }
            = new ConcurrentDictionary<string, RestTestResult>();

        public void OnFinished(RestTestResult testResult)
        {
            Results[testResult.TestName] = testResult;
        }
    }
}
