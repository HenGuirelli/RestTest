using RestTest.Library.Entity.Http;

namespace RestTest.Library.Entity.Test.TestEvaluator
{
    class HeaderEvaluator : TestEvaluator
    {
        public override void Evaluate(Validation validation, Response response)
        {
            if (!validation.Header.HasValue) return;

            Validate(response.Header.Equals(validation.Header),
                FormatMessage($"Header => expected {validation.Header} received {response.Header}"));
        }
    }
}
