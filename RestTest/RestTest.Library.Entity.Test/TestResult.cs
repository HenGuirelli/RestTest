using RestTest.Library.Entity.Http;
using RestTest.Library.Entity.Test.TestEvaluator;
using System.Collections.Generic;
using System.Linq;

namespace RestTest.Library.Entity.Test
{
    public class TestResult
    {
        public string TestName { get; private set; }
        public Status Status { get; private set; } = Status.Ok;
        public Response Response { get; private set; }
        public Validation Validation { get; }

        private const string DefaultName = "<No Name>";

        public string Error => string.Join($"\n{TestName}: ", _errorList);
        public string Text => Status == Status.Ok ? $"{TestName}: {Status}" : $"{TestName}: {Status} {Error}";

        private readonly List<string> _errorList = new List<string>();
        private readonly List<ITestEvaluator> _testsEvaluators = new List<ITestEvaluator>();

        public TestResult(string testName, Validation validation, Response response)
        {
            TestName = string.IsNullOrWhiteSpace(testName) ? DefaultName : testName;
            Response = response;
            Validation = validation;

            FillEvaluators();
            Evaluate();
        }

        private void FillEvaluators()
        {
            _testsEvaluators.Add(new BodyEvaluator());
            _testsEvaluators.Add(new CookieEvaluator());
            _testsEvaluators.Add(new HeaderEvaluator());
            _testsEvaluators.Add(new StatusEvaluator());
            _testsEvaluators.Add(new TestResultStatusEvaluator(this));
        }

        private void Evaluate()
        {
            foreach (var evaluator in _testsEvaluators)
            {
                evaluator.Evaluate(Validation, Response);
                if (evaluator.Error)
                {
                    _errorList.AddRange(evaluator.Errors);
                    Status = Status.Fail;
                }
            }
        }
    }
}
