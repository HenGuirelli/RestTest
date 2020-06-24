using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RestTest.Library.Config
{
    public class HeaderConfig : Dictionary<string, string>, IEquatable<HeaderConfig>
    {
        public static HeaderConfig Empty => new HeaderConfig();
        public bool HasValue { get; private set; }

        public HeaderConfig()
        {
        }

        public HeaderConfig(IDictionary<string, string> dictionary) : base(dictionary)
        {
            HasValue = dictionary.Any();
        }

        public HeaderConfig(WebHeaderCollection header)
        {
            foreach (var headerkey in header.AllKeys)
            {
                Add(headerkey, header[headerkey]);
            }
        }

        public new void Add(string key, string value)
        {
            HasValue = true;
            base.Add(key, value);
        }

        public bool Equals(HeaderConfig other)
        {
            if (other is null) return false;
            if (other.Count != this.Count) return false;

            foreach (var item in other)
            {
                if (!TryGetValue(item.Key, out var _))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return $"{{ {string.Join(", ", this.Select(x => $"{x.Key}: {x.Value}"))} }}";
        }
    }
}