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

        public TestResult(string testName, Validation validation, Response result)
        {
            TestName = testName;
            if (validation.Status.HasValue)
            {
                Validate(result.Status == validation.Status, $"Status => expected {validation.Status}\nreceived {result.Status}");
            }

            if (validation.Body.HasValue)
            {
                Validate(result.Body.Compare(validation.Body), $"Body => expected {validation.Body}\nreceived {result.Body}");
            }

            if (validation.Cookies.HasValue)
            {
                Validate(result.Cookies.Equals(validation.Cookies), $"Cookie => expected {validation.Cookies}\nreceived {result.Cookies}");
            }

            if (validation.Header.HasValue)
            {
                Validate(result.Header.Equals(validation.Header), $"Header => expected {validation.Header}\nreceived {result.Header}");
            }

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
