using System.Collections.Generic;
using System.Linq;

namespace RestTest.NewJsonHelper
{
    public class JsonList : JsonAttribute
    {
        private readonly List<Json> _list = new List<Json>();

        public Json this[int index] => _list[index];

        public JsonList() { }
        public JsonList(string key)
        {
            Key = key;
        }

        public void Add(params Json[] json)
        {
            _list.AddRange(json);
        }

        public void Remove(Json json)
        {
            _list.Remove(json);
        }

        public override object GetValue()
        {
            return _list;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as JsonList);
        }

        public bool Equals(JsonList other)
        {
            if (other is null) return false;
            if (_list.Count != _list.Count) return false;

            foreach(var item in _list)
            {
                if (!other._list.Contains(item)) return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"[{ string.Join(", ", _list) }]";
        }
    }
}
