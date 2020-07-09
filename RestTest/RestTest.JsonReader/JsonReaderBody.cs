using RestTest.Library.Entity.Http;

namespace RestTest.JsonReader
{
    public class JsonReaderBody : JsonReader<Body>
    {
        protected override Body Empty()
        {
            return Body.Empty;
        }
    }
}
