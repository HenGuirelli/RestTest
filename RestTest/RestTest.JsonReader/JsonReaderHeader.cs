using RestTest.Library.Entity.Http;
using System.Net;

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
