using RestTest.Library.Entity;
using RestTest.Library.Entity.Test;

namespace RestTest.Library.SequenceDependency
{
    internal interface IEvaluate
    {
        string Evaluate(string value, TestResult result);
    }
}