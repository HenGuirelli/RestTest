using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RestTest.Configuration
{
    public class CookiesConfig : Dictionary<string, string>, IEquatable<CookiesConfig>
    {

        public static CookiesConfig Empty => new CookiesConfig();
        public bool HasValue { get; private set; }

        public CookiesConfig()
        {
        }

        public CookiesConfig(IDictionary<string, string> dictionary) : base(dictionary)
        {
            HasValue = dictionary.Any();
        }

        public CookiesConfig(CookieCollection cookies)
        {
            foreach(Cookie cook in cookies)
            {
                Add(cook.Name, cook.Value);
            }
        }

        public new void Add(string key, string value)
        {
            HasValue = true;
            base.Add(key, value);
        }

        public bool Equals(CookiesConfig other)
        {
            if (other is null) return false;
            if (other.Count != this.Count) return false;

            foreach(var item in other)
            {
                if(!TryGetValue(item.Key, out var _))
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
