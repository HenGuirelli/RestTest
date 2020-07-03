using RestTest.Library.Entity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RestTest.Library.SequenceDependency
{
    public class DependencyDetector
    {
        private readonly Dictionary<DepedencyType, IEvaluate> _map 
            = new Dictionary<DepedencyType, IEvaluate>();

        public DependencyDetector()
        {
            _map.Add(DepedencyType.Body, new EvaluateBody());
        }

        public bool IsDependency(string value)
        {
            return Regex.IsMatch(value, @"\$\{.*\..*\}");
        }

        public string GetDependencyName(string value)
        {
            try
            {
                var initMatch = "${";
                var indexInit = value.IndexOf(initMatch);
                return value.Substring(indexInit + initMatch.Length, value.IndexOf(".") - initMatch.Length - indexInit);
            }
            catch
            {
                throw new ArgumentException("value is not a dependency");
            }
        }

        public string Evaluate(string value, TestResult result)
        {
            DepedencyType type = GetDependencyType(value);
            return _map[type].Evaluate(value, result);
        }

        private DepedencyType GetDependencyType(string value)
        {
            if (Regex.IsMatch(value, @"\.response\.body\."))
            {
                return DepedencyType.Body;
            }
            return default;
        }

        private IEnumerable<string> GetBodyAttributes(string value)
        {
            var match = Regex.Match(value, @"\.body\.([^\}]*)");
            return match.Groups[1].Value.Split('.');
        }

        private bool IsBodyDependency(string value)
        {
            return value.Contains(".body.");
        }
    }
}
