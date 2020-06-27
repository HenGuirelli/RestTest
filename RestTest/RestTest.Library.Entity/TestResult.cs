using System.Collections.Generic;
using System.Linq;

namespace RestTest.Library.Entity
{
    public class TestResult
    {
        public string TestName { get; private set; }
        public Status Status { get; private set; }
        public string Error => string.Join("\n", _errorList);

        public string Text => Status == Status.Ok ? $"{TestName}: {Status}" : $"{TestName}: {Status} {Error}";

        private const string DefaultName = "<No Name>";

        private readonly List<string> _errorList = new List<string>();

        public TestResult(string testName, Validation validation, Response result)
        {
            TestName = string.IsNullOrWhiteSpace(testName) ? DefaultName : testName;
            
            Validate(string.IsNullOrWhiteSpace(result.Error),
                FormatMessage($"General error => {result.Error}"));
            

            if (validation.Status.HasValue)
            {
                Validate(result.Status == validation.Status, 
                    FormatMessage($"Status => expected {validation.Status} received {result.Status}"));
            }

            if (validation.Body.HasValue)
            {
                Validate(result.Body.Compare(validation.Body),
                    FormatMessage($"Body => expected {validation.Body} received {result.Body}"));
            }

            if (validation.Cookies.HasValue)
            {
                Validate(result.Cookies.Equals(validation.Cookies),
                    FormatMessage($"Cookie => expected {validation.Cookies} received {result.Cookies}"));
            }

            if (validation.Header.HasValue)
            {
                Validate(result.Header.Equals(validation.Header),
                    FormatMessage($"Header => expected {validation.Header} received {result.Header}"));
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
