using RestTest.Library.Entity.Http;

namespace RestTest.Library.Entity.Test.TestEvaluator
{
    class CookieEvaluator : TestEvaluator
    {
        public override void Evaluate(Validation validation, Response response)
        {
            if (!validation.Cookies.HasValue) return;

            Validate(response.Cookies.Equals(validation.Cookies),
                FormatMessage($"Cookie => expected {validation.Cookies} received {response.Cookies}"));
        }
    }
}
