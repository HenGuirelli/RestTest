using System.Collections.Generic;

namespace RestTest.Configuration
{
    public class SequenceConfiguration
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public List<UniqueConfiguration> Sequence { get; private set; }

        public SequenceConfiguration(string name, string type, List<UniqueConfiguration> sequence)
        {
            Name = name;
            Type = type;
            Sequence = sequence;
        }
    }
}