using RestTest.Library.Entity.Test;
using System;
using System.IO;

namespace RestTest.ConsoleApp.Output
{
    internal class OutputFile : IOutput
    {
        static readonly object _lockFile = new object();
        private readonly ArgsResult _argsResult;

        public OutputFile(ArgsResult argsResult)
        {
            _argsResult = argsResult;
            CleanResultFile();
        }

        private void CleanResultFile()
        {
            File.WriteAllText(_argsResult.ResultPath, "");
        }

        public void OnTestFinished(TestResult result)
        {
            lock (_lockFile)
            {
                File.AppendAllText(_argsResult.ResultPath, $"{result.Text}\n");
            }
        }

        public void AllTestsFinished()
        {
            if (_argsResult.Verbose)
            {
                Console.WriteLine($"All tests finished. See result in: {_argsResult.ResultPath}");
            }
        }
    }
}
