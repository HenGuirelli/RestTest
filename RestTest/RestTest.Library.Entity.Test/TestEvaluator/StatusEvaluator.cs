using RestTest.Library.Entity.Http;

namespace RestTest.Library.Entity.Test.TestEvaluator
{
    class StatusEvaluator : TestEvaluator
    {
        public override void Evaluate(Validation validation, Response response)
        {
            if (!validation.Status.HasValue) return;

            Validate(response.Status == validation.Status,
                FormatMessage($"Status => expected {validation.Status} received {response.Status}"));
        }
    }
}
