using RestTest.Library.Entity.Http;

namespace RestTest.Library.Entity.Test.TestEvaluator
{
    class TestResultStatusEvaluator : TestEvaluator
    {
        private readonly Status _status;

        public TestResultStatusEvaluator(Status status)
        {
            _status = status;
        }

        public override void Evaluate(Validation validation, Response response)
        {
            if (_status == Status.Fail)
            {
                Validate(string.IsNullOrWhiteSpace(response.Error),
                    FormatMessage($"General error => {response.Error}"));
            }
        }
    }
}
