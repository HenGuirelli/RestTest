using RestTest.Library.Entity.Test;

namespace RestTest.ConsoleApp.Output
{
    internal interface IOutput
    {
        void OnTestFinished(TestResult result);
        void AllTestsFinished();
    }
}
