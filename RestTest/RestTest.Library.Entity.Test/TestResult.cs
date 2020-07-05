using RestTest.Library.Entity.Http;
using System.Collections.Generic;
using System.Linq;

namespace RestTest.Library.Entity.Test
{
    public class TestResult
    {
        public string TestName { get; private set; }
        public Status Status { get; private set; }
        public Response Response { get; private set; }
        public string Error => string.Join("\n", _errorList);

        public string Text => Status == Status.Ok ? $"{TestName}: {Status}" : $"{TestName}: {Status} {Error}";

        private const string DefaultName = "<No Name>";

        private readonly List<string> _errorList = new List<string>();

        public TestResult(string testName, Validation validation, Response response)
        {
            TestName = string.IsNullOrWhiteSpace(testName) ? DefaultName : testName;
            Response = response;

            Validate(string.IsNullOrWhiteSpace(response.Error),
                FormatMessage($"General error => {response.Error}"));

            if (validation.Status.HasValue)
            {
                Validate(response.Status == validation.Status,
                    FormatMessage($"Status => expected {validation.Status} received {response.Status}"));
            }

            if (validation.Body.HasValue)
            {
                Validate(response.Body.Equals(validation.Body),
                    FormatMessage($"Body => expected {validation.Body} received {response.Body}"));
            }

            if (validation.Cookies.HasValue)
            {
                Validate(response.Cookies.Equals(validation.Cookies),
                    FormatMessage($"Cookie => expected {validation.Cookies} received {response.Cookies}"));
            }

            if (validation.Header.HasValue)
            {
                Validate(response.Header.Equals(validation.Header),
                    FormatMessage($"Header => expected {validation.Header} received {response.Header}"));
            }

            Status = _errorList.Any() ? Status.Fail : Status.Ok;
        }

        private string FormatMessage(string str)
        {
            var textFormatter = new TextFormatter(str);
            return textFormatter
                .RemoveNewLine()
                .RemoveEspecialCharacters()
                .RemoveMultipleSpaces()
                .Trim()
                .ToString();
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
