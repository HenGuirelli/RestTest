using RestTest.Library.Entity.Http;

namespace RestTest.JsonReader
{
    public interface IJsonReader
    {
        Body Read(string json);
        Body ReadByFile(string path);
    }
}
