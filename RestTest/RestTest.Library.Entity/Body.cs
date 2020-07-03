using RestTest.NewJsonHelper;

namespace RestTest.Library.Entity
{
    public class Body : JsonObject
    {
        public new static Body Empty => new Body() { HasValue = false };

        public bool HasValue { get; private set; }

        public Body()
        {
            HasValue = true;
        }
    }
}
