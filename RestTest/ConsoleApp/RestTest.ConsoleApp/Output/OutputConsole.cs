using RestTest.Library.Entity.Test;
using System;

namespace RestTest.ConsoleApp.Output
{
    internal class OutputConsole : IOutput
    {
        static readonly object _lockFile = new object();

        public void AllTestsFinished()
        {
            Console.WriteLine($"All tests finished");
        }

        public void OnTestFinished(TestResult result)
        {
            lock (_lockFile)
            {
                Console.WriteLine($"{result.Text}");
            }
        }
    }
}
