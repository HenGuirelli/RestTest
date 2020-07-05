using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RestTest.Library.Entity.Http
{
    public class Cookies : Dictionary<string, string>, IEquatable<Cookies>
    {
        public static Cookies Empty => new Cookies();
        public bool HasValue { get; private set; }

        public Cookies()
        {
        }

        public Cookies(IDictionary<string, string> dictionary) : base(dictionary)
        {
            HasValue = dictionary.Any();
        }

        public Cookies(CookieCollection cookies)
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

        public bool Equals(Cookies other)
        {
            if (other is null) return false;
            if (other.Count != this.Count) return false;

            foreach(var item in other)
            {
                if(!TryGetValue(item.Key, out var otherValue))
                {
                    return false;
                }
                if (item.Value != otherValue) return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{{ {string.Join(", ", this.Select(x => $"{x.Key}: {x.Value}"))} }}";
        }
    }
}
