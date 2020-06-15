using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RestTest.Library.Test
{
    internal class ClassTest : IRequestLifeClycle
    {
        public Action Action { get; set; }

        public void OnFinished(TestResult testResult)
        {
        }

        public void OnStart()
        {
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OnUseRestTest()
        {
            var requestCallbackMap = new RequestCallbackMap();
            requestCallbackMap["request1"] = new ClassTest
            {
                Action = () => { }
            };

            var restTest = new RT("./tests.json", requestCallbackMap);

            restTest.Start();
        }
    }
}
