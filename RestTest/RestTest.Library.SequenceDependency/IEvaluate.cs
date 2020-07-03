using RestTest.Library.Entity;

namespace RestTest.Library.SequenceDependency
{
    internal interface IEvaluate
    {
        string Evaluate(string value, TestResult result);
    }
}