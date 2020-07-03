using System.Collections.Generic;

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
    }
}
