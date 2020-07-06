using RestTest.Library.Entity.Test;
using System;

namespace RestTest.ConsoleApp.Output
{
    internal class OutputConsole : IOutput
    {
        public void AllTestsFinished()
        {
            Console.WriteLine($"All tests finished");
        }

        public void OnTestFinished(TestResult result)
        {
            Console.WriteLine($"{result.Text}");
        }
    }
}
