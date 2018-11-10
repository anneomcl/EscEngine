using System;
using System.Collections.Generic;

namespace Esckie
{
    public class EscCommand
    {
        public EscCommand()
        {
            Children = new List<EscCommand>();
            Parameters = new List<string>();
            Conditions = new VmCondition();
        }

        public EscCommand(string name)
        {
            Name = name;
            Children = new List<EscCommand>();
            Parameters = new List<string>();
            Conditions = new VmCondition();
        }

        public string Name { get; set; }
        public IList<EscCommand> Children { get; set; }
        public List<string> Parameters { get; set; }
        public VmCondition Conditions { get; set; }
    }

    public class VmCondition
    {
        public Dictionary<string, bool> IfTrue = new Dictionary<string, bool>();
        public Dictionary<string, bool> IfFalse = new Dictionary<string, bool>();
    }
}