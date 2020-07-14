using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestTest.Configuration;
using RestTest.Library.Entity;
using RestTest.Library.Entity.Test;
using RestTest.Library.SequenceDependency;
using RestTest.RestRequest;

namespace RestTest.Library
{
    public class RT
    {
        private readonly RestTest.Configuration.Configuration _config;
        public TestFinishedHandle OnTestFinished;
        public TestStartHandle OnTestStart;

        public RT(string configPath)
        {
            _config = new RestTest.Configuration.Configuration(configPath);
        }

        public void Start()
        {
            var task = StartAsync();
            task.Wait();
        }

        private async Task StartAsync()
        {
            var sequenceDependency = new SequenceDependencyLocator();
            var tasks = new List<Task>();
            foreach (var item in _config.Uniques)
            {
                var task = new Task<TestResult>(() => StartTest(sequenceDependency, item).Result);                
                sequenceDependency.Register(item.Name, task);
                tasks.Add(task);
            }

            foreach (var task in tasks)
            {
                task.Start();
            }

            await Task.WhenAll(tasks);
        }

        private async Task<TestResult> StartTest(SequenceDependencyLocator sequenceDependency, UniqueConfiguration item)
        {
            var requestConfig = item.ToRequestConfig();
            await sequenceDependency.ReplaceDependency(requestConfig);
            await sequenceDependency.ReplaceDependency(item.Validation);
            var request = Requests.Create(requestConfig);
            OnTestStart?.Invoke(item.Name);
            var response = request.Send();
            var testResult = new TestResult(item.Name, item.Validation, await response);
            OnTestFinished?.Invoke(testResult);
            return testResult;
        }
    }
}
