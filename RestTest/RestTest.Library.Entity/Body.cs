using RestTest.JsonHelper;
using System;
using System.Collections.Generic;

namespace RestTest.Library.Entity
{
    public class Body : Json, IEquatable<Body>
    {
        public Body(string json) : base(json)
        {
        }

        public bool Equals(Body other)
        {
            if (other is null) return false;

            IEnumerable<string> keys = Keys;
            foreach (var key in keys)
            {
                JsonValue attrBody = other[key];

                if (attrBody.ToString() == "${ANY}") continue;

                if (attrBody.IsObject)
                {
                    if (!this[key].Equals(attrBody)) return false;
                }
                else
                {
                    if (!attrBody.Equals(this[key])) return false;
                }
            }

            return true;
        }
    }
}
