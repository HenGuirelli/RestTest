using RestTest.Library.Entity.Http;

namespace RestTest.JsonReader
{
    public class JsonReaderCookie : JsonReader<Cookies>
    {
        protected override Cookies Empty()
        {
            return Cookies.Empty;
        }
    }
}
