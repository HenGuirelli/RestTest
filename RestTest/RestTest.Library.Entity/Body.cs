﻿using RestTest.NewJsonHelper;

namespace RestTest.Library.Entity
{
    public class Body : JsonObject
    {
        public new static Body Empty => new Body();

        public bool HasValue;

        public bool Equals(Body other)
        {
            return true;
            //if (other is null) return false;

            //foreach (var key in Keys)
            //{
            //    JsonValue attrBody = default;
            //    try 
            //    {
            //        attrBody = other[key];
            //    }
            //    catch
            //    {
            //        return false;
            //    }

            //    if (attrBody.ToString() == "${ANY}" || this[key].ToString() == "${ANY}") continue;
                
            //    if (attrBody.ToString() == "${NUMBER}")
            //    {
            //        if (long.TryParse(this[key].ToString(), out var _)) continue;
            //    }
            //    else if (this[key].ToString() == "${NUMBER}")
            //    {
            //        if (long.TryParse(attrBody.ToString(), out var _)) continue;
            //    }

            //    Match regexResultAttrBody = Regex.Match(attrBody.ToString(), @"\${Regex: (.*)}");
            //    Match thisAttrBody = Regex.Match(this[key].ToString(), @"\${Regex: (.*)}");
            //    if (regexResultAttrBody.Success)
            //    {
            //        if (Regex.Match(this[key].ToString(), regexResultAttrBody.Groups[1].Value).Length > 0) continue;
            //    }
            //    else if (thisAttrBody.Success)
            //    {
            //        if (Regex.Match(attrBody.ToString(), thisAttrBody.Groups[1].Value).Length > 0) continue;
            //    }

            //    if (attrBody.IsObject)
            //    {
            //        if (!new Body(this[key].ToString()).Equals(new Body(attrBody.ToString()))) return false;
            //    }
            //    else
            //    {
            //        if (!attrBody.Equals(this[key])) return false;
            //    }
            //}

            //return true;
        }
    }
}
