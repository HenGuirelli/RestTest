using RestTest.Library.Entity.Test;
using RestTest.NewJsonHelper;
using System;
using System.Text.RegularExpressions;

namespace RestTest.Library.SequenceDependency.Evaluators
{
    public abstract class AbstractEvaluator : IEvaluate
    {
        public string Evaluate(string value, TestResult result)
        {
            string[] indexes = GetBodyIndexes(value);

            JsonAttribute json = GetJsonAttributeToEvaluate(result);
            foreach (var index in indexes)
            {
                json = json[index];
            }

            var returnValue = json?.GetValue()?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(returnValue))
            {
                throw new ArgumentException($"Depedency {value} not found");
            }
            return returnValue;
        }

        private string[] GetBodyIndexes(string value)
        {
            var match = Regex.Match(value, GetBodyIndexRegex());
            return match.Groups[1].Value.Split('.');
        }

        protected abstract string GetBodyIndexRegex();
        protected abstract JsonAttribute GetJsonAttributeToEvaluate(TestResult result);
    }
}
