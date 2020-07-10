using RestTest.Library.Entity.Http;

namespace RestTest.JsonReader
{
    public class JsonReaderQueryString : JsonReader<QueryString>
    {
        protected override QueryString Empty()
        {
            return QueryString.Empty;
        }
    }
}
