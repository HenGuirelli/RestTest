using System;
using System.Collections.Generic;
using System.Linq;

namespace RestTest.ConsoleApp
{
    internal class ArgsResult
    {
        private readonly List<string> _helpOptions = new List<string>() { "help", "-help", "-h", "" };
        public bool IsHelp { get; private set; }
        public string ConfigPath { get; private set; }
        public bool OutputInConsole { get; }
        public string ResultPath { get; private set; }
        public bool ContinueAfterFinished { get; private set; }
        private const string ContinueAfterFinishedArgument = "-c";

        public ArgsResult(string[] args)
        {
            ContinueAfterFinished = args.Contains(ContinueAfterFinishedArgument);

            args = RemoveOptions(args);

            if (args.Length == 0 ||
                (args.Length == 1 && _helpOptions.Contains(args[0])))
            {
                IsHelp = true;
                return;
            }

            if (args.Length == 2)
            {
                ConfigPath = args[0];
                ResultPath = args[1];
                return;
            }

            if (args.Length == 1)
            {
                ConfigPath = args[0];
                OutputInConsole = true;
                return;
            }

            IsHelp = true;
        }

        private string[] RemoveOptions(string[] args)
        {
            return args.Where(x => !x.StartsWith("-")).ToArray();
        }
    }
}