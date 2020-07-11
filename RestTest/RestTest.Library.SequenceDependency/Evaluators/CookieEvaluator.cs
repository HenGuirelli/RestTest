using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;

namespace RestTest.Library.SequenceDependency.Evaluators
{
    public class CookieEvaluator : AbstractEvaluator
    {
        protected override string GetBodyIndexRegex()
        {
            return @"\.cookies?\.(.*)\}";
        }

        protected override JsonAttribute GetJsonAttributeToEvaluate(TestResult result)
        {
            return result.Response.Cookies;
        }
    }
}
