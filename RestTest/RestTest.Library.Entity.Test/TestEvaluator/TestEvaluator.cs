using RestTest.Library.Entity.Http;
using System.Collections.Generic;

namespace RestTest.Library.Entity.Test.TestEvaluator
{
    internal abstract class TestEvaluator : ITestEvaluator
    {
        public bool Error { get; protected set; }
        public IEnumerable<string> Errors => _errorList;
        public List<string> _errorList = new List<string>();

        public abstract void Evaluate(Validation validation, Response response);

        protected void Validate(bool condition, string error)
        {
            Error = !condition;
            if (Error)
            {
                _errorList.Add(error);
            }
        }

        protected string FormatMessage(string str)
        {
            var textFormatter = new TextFormatter(str);
            return textFormatter
                .RemoveNewLine()
                .RemoveEspecialCharacters()
                .RemoveMultipleSpaces()
                .Trim()
                .ToString();
        }
    }
}
