using System;

namespace RestTest.NewJsonHelper
{
    public abstract class JsonAttribute : Json
    {
        public virtual JsonAttribute this[string key] => throw new ArgumentException("index not available");
        public string Key { get; protected set; }
        public override abstract object GetValue();
    }
}
