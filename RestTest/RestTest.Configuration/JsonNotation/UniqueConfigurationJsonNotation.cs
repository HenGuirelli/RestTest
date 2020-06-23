namespace RestTest.Configuration.JsonNotation
{
    internal class UniqueConfigurationJsonNotation
    {
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string method { get; set; }
        public object header { get; set; }
        public object cookies { get; set; }
        public object body { get; set; }
        public object query_string { get; set; }

        public ValidationConfigJsonNotation validation { get; set; }
    }

    internal class ValidationConfigJsonNotation
    {
        public object body { get; set; }
        public object header { get; set; }
        public object query_string { get; set; }
        public object cookies { get; set; }
        public int status { get; set; }
        public int max_time { get; set; }
        public int min_time { get; set; }
    }
}
