namespace RestTest.Library
{
    public class TestResult
    {
        public Status Status { get; private set; }
        public string Error { get; private set; }

        public TestResult(Status status, string error)
        {
            Status = status;
            Error = error;
        }
    }
}
