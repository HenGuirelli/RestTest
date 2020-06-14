using System;
using System.Collections.Generic;
using System.Text;

namespace RestTest.Configuration.JsonNotation
{
    internal class SequenceConfigurationJsonNotation
    {
        public string name { get; set; }
        public string type { get; set; }

        public List<UniqueConfigurationJsonNotation> sequence { get; set; }
    }
}
