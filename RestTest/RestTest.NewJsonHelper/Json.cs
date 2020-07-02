using System;

namespace RestTest.NewJsonHelper
{
    public abstract class Json : IEquatable<Json>
    {
        public bool Equals(Json other)
        {
            if (other is null) return false;
            if (GetType().Name != other.GetType().Name) return false;

            return GetValue() == other.GetValue();
        }

        public abstract object GetValue();
    }
}
