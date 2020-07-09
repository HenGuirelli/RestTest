using RestTest.Library.Entity.Test;
using System;

namespace RestTest.ConsoleApp.Output
{
    internal class OutputConsole : IOutput
    {
        private readonly ArgsResult _argsResult;

        public OutputConsole(ArgsResult argsResult)
        {
            _argsResult = argsResult;
        }

        public void AllTestsFinished()
        {
            if (_argsResult.Verbose)
            {
                Console.WriteLine($"All tests finished");
            }
        }

        public void OnTestFinished(TestResult result)
        {
            Console.WriteLine($"{result.Text}");
        }
    }
}
