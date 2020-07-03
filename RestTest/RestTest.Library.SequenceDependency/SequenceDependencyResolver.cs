using RestTest.Library.Entity;
using RestTest.RestRequest;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RestTest.Library.SequenceDependency
{
    public class SequenceDependencyResolver
    {
        private readonly Dictionary<string, TestResult> _dict = new Dictionary<string, TestResult>();
        private readonly DependencyDetector _dependencyDetector = new DependencyDetector();

        public void ReplaceDependency(RequestConfig requestConfig)
        {
            requestConfig.QueryString = ReplaceDependecyQueryString(requestConfig.QueryString);
            requestConfig.Body = ReplaceDependecyBody(requestConfig.Body);
        }

        private string ReplaceDependecyBody(string bodyStr)
        {
            //var body = new Body(bodyStr);
            //foreach (var item in body.Keys)
            //{
            //    if (body[item].IsObject)
            //    {
            //        JsonHelper.JsonValue attribute = default;
            //        while ((attribute = body[item]).IsObject)
            //        {

            //        }
            //    }
            //    else
            //    {
            //    }
            //}
            throw new NotImplementedException();
        }

        private Dictionary<string, string> ReplaceDependecyQueryString(IDictionary<string, string> queryString)
        {
            var r = new Dictionary<string, string>();
            foreach (var item in queryString)
            {
                if (_dependencyDetector.IsDependency(item.Value))
                {
                    string name = _dependencyDetector.GetDependencyName(item.Value);
                    if(_dict.TryGetValue(name, out var result))
                    {
                        var valueToReplace = _dependencyDetector.EvaluateDependency(item.Value, result);
                        r[item.Key] = valueToReplace;
                    }
                }
                else
                {
                    r[item.Key] = item.Value;
                }
            }
            return r;
        }

        public void AddResult(TestResult testResult)
        {
            _dict[testResult.TestName] = testResult;
        }
    }
}
