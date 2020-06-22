using RestTest.Library.Config;
using RestTest.RestRequest;
using System.Collections.Generic;
using System.Linq;

namespace RestTest.Library
{
    public class TestResult
    {
        public string TestName { get; private set; }
        public Status Status { get; private set; }
        public string Error => string.Join("\n\n", _errorList);
        private readonly List<string> _errorList = new List<string>();

        public TestResult(string testName, ValidationConfig validation, Response result)
        {
            TestName = testName;
            Validate(result.Status == validation.Status, $"Status => expected {validation.Status} received {result.Status}");
            Validate(result.Body.Compare(validation.Body), $"Body => expected {validation.Body} received {result.Body}");

            Status = _errorList.Any() ? Status.Fail : Status.Ok;
        }

        private void Validate(bool condition, string error)
        {
            if (!condition)
            {
                _errorList.Add(error);
            }
        }
    }
}
