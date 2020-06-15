using System;

namespace RestTest.Library
{
    public interface IRequestLifeClycle
    {
        Action Action { get; set; }
        void OnStart();
        void OnFinished(TestResult testResult);
    }
}