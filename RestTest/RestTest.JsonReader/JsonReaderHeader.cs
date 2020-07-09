using RestTest.Library.Entity.Http;

namespace RestTest.JsonReader
{
    public class JsonReaderHeader : JsonReader<Header>
    {
        protected override Header Empty()
        {
            return Header.Empty;
        }
    }
}
