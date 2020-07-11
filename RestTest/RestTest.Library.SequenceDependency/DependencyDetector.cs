using RestTest.Library.Entity.Test;
using RestTest.Library.SequenceDependency.Evaluators;
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
            _map.Add(DepedencyType.Body, new BodyEvaluator());
            _map.Add(DepedencyType.Header, new HeaderEvaluator());
            _map.Add(DepedencyType.Cookie, new CookieEvaluator());
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

            if (Regex.IsMatch(value, @"\.response\.header\."))
            {
                return DepedencyType.Header;
            }

            if (Regex.IsMatch(value, @"\.response\.cookies\."))
            {
                return DepedencyType.Cookie;
            }

            if (Regex.IsMatch(value, @"\.response\.query_string\."))
            {
                return DepedencyType.QueryString;
            }

            return default;
        }
    }
}
