using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using System.Text.RegularExpressions;

namespace RestTest.Library.SequenceDependency.Evaluators
{
    public class BodyEvaluator : IEvaluate
    {
        public string Evaluate(string value, TestResult result)
        {
            string[] indexes = GetBodyIndexes(value);

            JsonAttribute json = result.Response.Body;
            foreach(var index in indexes)
            {
                json = json[index];
            }
            return json.GetValue().ToString();
        }

        private string[] GetBodyIndexes(string value)
        {
            var match = Regex.Match(value, @"\.body\.(.*)\}");
            return match.Groups[1].Value.Split('.');
        }
    }
}