using RestTest.JsonHelper;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RestTest.Library.Entity
{
    public class Body : Json, IEquatable<Body>
    {
        public new static Body Empty => new Body("{}");

        public Body(string json) : base(json)
        {
        }

        public bool Equals(Body other)
        {
            if (other is null) return false;

            IEnumerable<string> keys = Keys;
            foreach (var key in keys)
            {
                JsonValue attrBody = default;
                try 
                {
                    attrBody = other[key];
                }
                catch
                {
                    return false;
                }

                if (attrBody.ToString() == "${ANY}" || this[key].ToString() == "${ANY}") continue;
                
                if (attrBody.ToString() == "${NUMBER}")
                {
                    if (long.TryParse(this[key].ToString(), out var _)) continue;
                }
                else if (this[key].ToString() == "${NUMBER}")
                {
                    if (long.TryParse(attrBody.ToString(), out var _)) continue;
                }

                Match regexResultAttrBody = Regex.Match(attrBody.ToString(), @"\${Regex: (.*)}");
                Match thisAttrBody = Regex.Match(this[key].ToString(), @"\${Regex: (.*)}");
                if (regexResultAttrBody.Success)
                {
                    if (Regex.Match(this[key].ToString(), regexResultAttrBody.Groups[1].Value).Length > 0) continue;
                }
                else if (thisAttrBody.Success)
                {
                    if (Regex.Match(attrBody.ToString(), thisAttrBody.Groups[1].Value).Length > 0) continue;
                }

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
