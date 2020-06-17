using RestTest.RestRequest;

namespace RestTest.Library
{
    public interface IRequestLifeClycle
    {
        Requests Request { get; set; }
        void OnStart();
        void OnFinished(TestResult testResult);
    }
}