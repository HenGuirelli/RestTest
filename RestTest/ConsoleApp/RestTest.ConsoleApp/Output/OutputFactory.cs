namespace RestTest.ConsoleApp.Output
{
    internal class OutputFactory
    {
        public IOutput Create(ArgsResult argsResult)
        {
            if (argsResult.OutputInConsole)
            {
                return new OutputConsole(argsResult);
            }
            return new OutputFile(argsResult);
        }
    }
}
