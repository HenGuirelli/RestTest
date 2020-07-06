﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task StartAsync()
        {
            IEnumerable<Task> tasksUniques = StartUniqueTests();
            IEnumerable<Task> tasksSequence = StartSequenceTests();

            await Task.WhenAll(tasksUniques);
            await Task.WhenAll(tasksSequence);
        }

        private IEnumerable<Task> StartSequenceTests()
        {
            return _config.Sequences.Select(async item =>
            {
                var sequenceDependency = new SequenceDependencyLocator();
                foreach (var sequeceItem in item.Sequence)
                {
                    var requestConfig = sequeceItem.ToRequestConfig();
                    sequenceDependency.ReplaceDependency(requestConfig);
                    sequenceDependency.ReplaceDependency(sequeceItem.Validation);
                    var request = Requests.Create(requestConfig);
                    OnTestStart?.Invoke(sequeceItem.Name);
                    var response = request.Send();
                    var testResult = new TestResult(sequeceItem.Name, sequeceItem.Validation, await response);
                    sequenceDependency.Register(testResult);
                    OnTestFinished?.Invoke(testResult);
                    if (testResult.Status == Status.Fail) return;
                }
            });
        }

        private IEnumerable<Task> StartUniqueTests()
        {
            return _config.Uniques.Select(async item =>
            {
                var request = Requests.Create(item.ToRequestConfig());
                OnTestStart?.Invoke(item.Name);
                var response = request.Send();
                var testResult = new TestResult(item.Name, item.Validation, await response);
                OnTestFinished?.Invoke(testResult);
            });
        }
    }
}
