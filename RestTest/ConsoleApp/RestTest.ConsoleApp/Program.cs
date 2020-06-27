﻿using RestTest.Library;
using System;
using System.IO;

namespace RestTest.ConsoleApp
{
    class Program
    {
        static string _resultPath;

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
                rt = new RT(argsResult.ConfigPath);
                rt.OnTestFinished += OnTestFinished;

                rt.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                Console.ReadKey();
            }
        }

        private static ArgsResult ParseArgs(string[] args)
        {
            return new ArgsResult(args);
        }

        private static void OnTestFinished(TestResult result)
        {
            if (result.Status == Status.Ok)
            {
                File.AppendAllText(_resultPath, $"{result.TestName}: {result.Status}");
            }
            else
            {
                File.AppendAllText(_resultPath, $"{result.TestName}: {result.Status} {result.Error}");
            }
        }
    }
}