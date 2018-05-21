using System;
using System.Collections.Generic;

namespace Esckie
{
    public class VmCommand
    {
        public VmCommand()
        {
            Links = new List<VmCommand>();
            Parameters = Array.Empty<string>();
            Conditions = new VmCondition();
        }

        public string Name { get; set; }
        public IList<VmCommand> Links { get; set; }
        public string[] Parameters { get; set; }
        public VmCondition Conditions { get; set; }

        public class VmCondition
        {
            public Dictionary<string, bool> IfTrue = new Dictionary<string, bool>();
            public Dictionary<string, bool> IfFalse = new Dictionary<string, bool>();
            public List<Tuple<string, string, string>> Conditions = new List<Tuple<string, string, string>>();
        }
    }
}