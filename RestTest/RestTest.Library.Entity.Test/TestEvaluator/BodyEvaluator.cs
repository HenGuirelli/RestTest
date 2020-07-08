using RestTest.Library.Entity.Http;

namespace RestTest.Library.Entity.Test.TestEvaluator
{
    internal class BodyEvaluator : TestEvaluator
    {
        public override void Evaluate(Validation validation, Response response)
        {
            if (!validation.Body.HasValue) return;

            Validate(response.Body.Equals(validation.Body),
                FormatMessage($"Body => expected {validation.Body} received {response.Body}"));
        }
    }
}
