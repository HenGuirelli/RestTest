namespace RestTest.Configuration.JsonNotation
{
    internal class UniqueConfigurationJsonNotation
    {
        public string name { get; set; }
        public string url { get; set; }
        public string method { get; set; }
        public object header { get; set; }
        public object cookies { get; set; }
        public object body { get; set; }
        public object query_string { get; set; }
        public string Wait { get; set; }

        public ValidationJsonNotation validation { get; set; }
    }

    internal class ValidationJsonNotation
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
