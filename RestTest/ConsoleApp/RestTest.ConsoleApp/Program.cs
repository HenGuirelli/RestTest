using RestTest.Library;
using RestTest.Library.Entity.Test;
using System;
using System.IO;
using System.Threading;

namespace RestTest.ConsoleApp
{
    class Program
    {
        static string _resultPath;
        static volatile bool _testFinished = false;
        static readonly object _lockFile = new object();

        static void Main(string[] args)
        {
            ArgsResult argsResult = ParseArgs(args);

            if (argsResult.IsHelp)
            {
                Console.WriteLine("Help:");
                Console.WriteLine("Use 2 arguments. First argument is path of config file, second argument is path of result test");
                Console.WriteLine("Example:");
                Console.WriteLine("\tRestTest.ConsoleApp C:\\\\folder\\config.json .\\result.json");

                Console.ReadKey();
                return;
            }

            _resultPath = argsResult.ResultPath;
            RT rt = default;
            try
            {
                CleanResultFile();
                rt = new RT(argsResult.ConfigPath);
                rt.OnTestFinished += OnTestFinished;

                rt.Start();
                rt.OnAllTestsFinished += AllTestsFinished;

                SpinWait.SpinUntil(() => _testFinished);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                Console.ReadKey();
            }
        }

        private static void AllTestsFinished()
        {
            Console.WriteLine($"All tests finished. See result in: {_resultPath}");
            Console.ReadKey();
            _testFinished = true;
        }

        private static void CleanResultFile()
        {
            File.WriteAllText(_resultPath, "");
        }

        private static ArgsResult ParseArgs(string[] args)
        {
            return new ArgsResult(args);
        }

        private static void OnTestFinished(TestResult result)
        {
            lock (_lockFile)
            {
                File.AppendAllText(_resultPath, $"{result.Text}\n");
            }
        }
    }
}
