using RestTest.Library.Entity.Test;
using System;
using System.IO;

namespace RestTest.ConsoleApp.Output
{
    internal class OutputFile : IOutput
    {
        static readonly object _lockFile = new object();
        private readonly string _resultPath;

        public OutputFile(string resultPath)
        {
            _resultPath = resultPath;
            CleanResultFile();
        }

        private void CleanResultFile()
        {
            File.WriteAllText(_resultPath, "");
        }

        public void OnTestFinished(TestResult result)
        {
            lock (_lockFile)
            {
                File.AppendAllText(_resultPath, $"{result.Text}\n");
            }
        }

        public void AllTestsFinished()
        {
            Console.WriteLine($"All tests finished. See result in: {_resultPath}");
        }
    }
}
