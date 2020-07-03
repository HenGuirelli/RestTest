using RestTest.Library.Entity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RestTest.Library.SequenceDependency
{
    public class DependencyDetector
    {
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
                return value.Substring(indexInit + initMatch.Length, value.IndexOf(".") - initMatch.Length);
            }
            catch
            {
                throw new ArgumentException("value is not a dependency");
            }
        }

        public string EvaluateDependency(string value, TestResult result)
        {
            //if (IsBodyDependency(value))
            //{
            //    IEnumerable<string> bodyAttributes = GetBodyAttributes(value);
            //    JsonValue attrResult = default;
            //    var first = true;
            //    foreach (var item in bodyAttributes)
            //    {
            //        if (first)
            //        {
            //            attrResult = result.Response.Body[item];
            //            first = false;
            //        }
            //        else
            //        {
            //            attrResult = attrResult[item];
            //        }
            //    }

            //    return attrResult.ToString();
            //}
            return string.Empty;
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
