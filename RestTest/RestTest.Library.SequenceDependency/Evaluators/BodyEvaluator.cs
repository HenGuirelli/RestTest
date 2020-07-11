using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;

namespace RestTest.Library.SequenceDependency.Evaluators
{
    public class BodyEvaluator : AbstractEvaluator
    {
        protected override string GetBodyIndexRegex()
        {
            return @"\.body\.(.*)\}";
        }

        protected override JsonAttribute GetJsonAttributeToEvaluate(TestResult result)
        {
            return result.Response.Body;
        }
    }
}