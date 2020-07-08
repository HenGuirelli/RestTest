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
        private readonly Queue<ITestEvaluator> _testsEvaluators = new Queue<ITestEvaluator>();

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
            _testsEvaluators.Enqueue(new BodyEvaluator());
            _testsEvaluators.Enqueue(new CookieEvaluator());
            _testsEvaluators.Enqueue(new HeaderEvaluator());
            _testsEvaluators.Enqueue(new StatusEvaluator());
            _testsEvaluators.Enqueue(new TestResultStatusEvaluator(this));
        }

        private void Evaluate()
        {
            while (_testsEvaluators.Any())
            {
                var evaluator = _testsEvaluators.Dequeue();
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
