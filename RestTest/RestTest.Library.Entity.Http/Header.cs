using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RestTest.Library.Entity.Http
{
    public class Header : Dictionary<string, string>, IEquatable<Header>
    {
        public static Header Empty => new Header();
        public bool HasValue { get; private set; }

        public Header() 
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public Header(IDictionary<string, string> dictionary) 
            : base(new Dictionary<string, string>(dictionary, StringComparer.OrdinalIgnoreCase))
        {
            HasValue = dictionary.Any();
        }

        public Header(WebHeaderCollection header)
            : base(StringComparer.OrdinalIgnoreCase)
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
                    if (value == "${NUMBER}" || item.Value == "${NUMBER}")
                    {
                        if (value == "${NUMBER}" && int.TryParse(item.Value, out var _)) continue;
                        if (item.Value == "${NUMBER}" && int.TryParse(value, out var _)) continue;
                    }
                    if (value != item.Value) return false;
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