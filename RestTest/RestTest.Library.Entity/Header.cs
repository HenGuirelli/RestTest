using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RestTest.Library.Entity
{
    public class Header : Dictionary<string, string>, IEquatable<Header>
    {
        public static Header Empty => new Header();
        public bool HasValue { get; private set; }

        public Header()
        {
        }

        public Header(IDictionary<string, string> dictionary) : base(dictionary)
        {
            HasValue = dictionary.Any();
        }

        public Header(WebHeaderCollection header)
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

        public bool Equals(Header other)
        {
            if (other is null) return false;
            if (other.Count != this.Count) return false;

            foreach (var item in other)
            {
                if (TryGetValue(item.Key, out var value))
                {
                    if (value == "${ANY}" || item.Value == "${ANY}") continue;
                }
                else
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