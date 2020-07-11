using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;

namespace RestTest.Library.SequenceDependency.Evaluators
{
    public class HeaderEvaluator : AbstractEvaluator
    {
        protected override string GetBodyIndexRegex()
        {
            return @"\.header\.(.*)\}";
        }

        protected override JsonAttribute GetJsonAttributeToEvaluate(TestResult result)
        {
            return result.Response.Header;
        }
    }
}