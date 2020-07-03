namespace RestTest.Library.Entity
{
    public interface IJsonReader
    {
        Body Create(string json);
        Body CreateByFile(string path);
    }
}
