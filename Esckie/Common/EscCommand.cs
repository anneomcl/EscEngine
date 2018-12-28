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
            Conditions = new Dictionary<string, bool>();
        }

        public EscCommand(string name)
        {
            Name = name;
            Children = new List<EscCommand>();
            Parameters = new List<string>();
            Conditions = new Dictionary<string, bool>();
        }

        public string Name { get; set; }
        public IList<EscCommand> Children { get; set; }
        public List<string> Parameters { get; set; }
        public Dictionary<string, bool> Conditions { get; set; }
    }
}