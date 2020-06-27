using System.Collections.Generic;

namespace RestTest.ConsoleApp
{
    internal class ArgsResult
    {
        private readonly List<string> _helpOptions = new List<string>() { "help", "-help", "-h", "" };
        public bool IsHelp { get; private set; }
        public string ConfigPath { get; private set; }
        public string ResultPath { get; private set; }

        public ArgsResult(string[] args)
        {
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

            IsHelp = true;
        }
    }
}