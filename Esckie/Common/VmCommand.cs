using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Creates a new VM command and assigns it to the correct parent.
        /// The current node is the default parent reference.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="indentLevel"></param>
        /// <param name="actions"></param>
        public void ProcessVmCommand(string line, int indentLevel, Dictionary<string, IList<Type>> actions)
        {
            var newCommand = new VmCommand();

            var tokens = EscCompilerHelpers.ParseLineToTokens(line);

            //Handle tokens that are condition flags

            //Ensure action is valid, and set the root's action
            if (!actions.Keys.Contains(tokens.First()))
            {
                throw new Exception();
            }

            newCommand.Name = tokens.First();
            tokens.RemoveAt(0);

            //Handle unary operators ">", "<", "?"
            //Handle dialog options

            //Set the root's parameters for the given action
            if (tokens.Count() != actions[newCommand.Name].Count())
            {
                throw new Exception();
            }

            newCommand.Parameters = tokens.ToArray();

            this.Links.Add(newCommand);
        }
    }
}