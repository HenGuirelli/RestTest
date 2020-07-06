using RestTest.ConsoleApp.Output;
using RestTest.Library;
using System;

namespace RestTest.ConsoleApp
{
    class Program
    {
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

            var factory = new OutputFactory();
            IOutput output = factory.Create(argsResult);

            try
            {
                RT rt = new RT(argsResult.ConfigPath);
                rt.OnTestFinished += output.OnTestFinished;
                rt.Start();
                output.AllTestsFinished();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static ArgsResult ParseArgs(string[] args)
        {
            return new ArgsResult(args);
        }
    }
}
