namespace RestTest.JsonReader
{
    public interface IJsonReader<out T>
    {
        T Read(string json);
        T ReadByFile(string path);
    }
}
