namespace RestTest.ConsoleApp.Output
{
    internal class OutputFactory
    {
        public IOutput Create(ArgsResult argsResult)
        {
            if (argsResult.OutputInConsole)
            {
                return new OutputConsole();
            }
            return new OutputFile(argsResult.ResultPath);
        }
    }
}
