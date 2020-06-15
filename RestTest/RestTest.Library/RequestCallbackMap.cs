using System;
using System.Collections;
using System.Collections.Generic;

namespace RestTest.Library
{
    public class RequestCallbackMap : IEnumerable<KeyValuePair<string, IRequestLifeClycle>>
    {
        private readonly Dictionary<string, IRequestLifeClycle> _cbMap = new Dictionary<string, IRequestLifeClycle>();

        public IRequestLifeClycle this[string key]
        {
            get => _cbMap[key];
            set => Add(key, value);
        }

        private void Add(string key, IRequestLifeClycle value)
        {
            _cbMap.Add(key, value);
        }

        public IEnumerator<KeyValuePair<string, IRequestLifeClycle>> GetEnumerator()
        {
            return _cbMap.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cbMap.GetEnumerator();
        }
    }
}
