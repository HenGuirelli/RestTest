using RestTest.Library.Entity;
using RestTest.Library.Entity.Test;

namespace RestTest.Library
{
    public delegate void TestFinishedHandle(TestResult result);
    public delegate void TestStartHandle(string testName);
}
