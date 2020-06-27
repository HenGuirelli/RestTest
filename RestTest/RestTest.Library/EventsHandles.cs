using RestTest.Library.Entity;

namespace RestTest.Library
{
    public delegate void TestFinishedHandle(TestResult result);
    public delegate void TestStartHandle(string testName);
    public delegate void TestAllTestFinishedHandle();
}
