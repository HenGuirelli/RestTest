namespace RestTest.NewJsonHelper
{
    public abstract class JsonAttribute : Json
    {
        public string Key { get; protected set; }
        public override abstract object GetValue();
    }
}
