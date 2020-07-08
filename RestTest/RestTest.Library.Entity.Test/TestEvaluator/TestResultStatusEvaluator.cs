using RestTest.Library.Entity.Http;

namespace RestTest.Library.Entity.Test.TestEvaluator
{
    class TestResultStatusEvaluator : TestEvaluator
    {
        private readonly TestResult _testResult;

        public TestResultStatusEvaluator(TestResult testResult)
        {
            _testResult = testResult;
        }

        public override void Evaluate(Validation validation, Response response)
        {
            if (_testResult.Status == Status.Fail)
            {
                Validate(string.IsNullOrWhiteSpace(response.Error),
                    FormatMessage($"General error => {response.Error}"));
            }
        }
    }
}
