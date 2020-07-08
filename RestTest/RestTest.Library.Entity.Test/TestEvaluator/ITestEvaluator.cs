using RestTest.Library.Entity.Http;
using System.Collections.Generic;

namespace RestTest.Library.Entity.Test.TestEvaluator
{
    public interface ITestEvaluator
    {
        bool Error { get; }
        IEnumerable<string> Errors { get; }
        void Evaluate(Validation validation, Response response);
    }
}
