using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using System.Text.RegularExpressions;

namespace RestTest.Library.SequenceDependency.Evaluators
{
    internal class HeaderEvaluator : IEvaluate
    {
        public string Evaluate(string value, TestResult result)
        {
            string[] indexes = GetIndexes(value);

            JsonAttribute json = result.Response.Header;
            foreach(var index in indexes)
            {
                json = json[index];
            }
            return json.GetValue().ToString();
        }

        private string[] GetIndexes(string value)
        {
            var match = Regex.Match(value, @"\.header\.(.*)\}");
            return match.Groups[1].Value.Split('.');
        }
    }
}