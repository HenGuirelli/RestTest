using RestTest.Library.Entity.Http;

namespace RestTest.Library.Entity
{
    public interface IJsonReader
    {
        Body Read(string json);
        Body ReadByFile(string path);
    }
}
