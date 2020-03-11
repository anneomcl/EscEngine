using System.Collections.Generic;

namespace Esckie
{
    public class EscVirtualMachine
    {
        private Dictionary<string, bool> globals;

        public EscVirtualMachine(Dictionary<string, bool> globals)
        {
            this.globals = globals;
        }

        public List<EscCommand> GetCommandSequence(EscCommand root)
        {
            var commandSequence = new List<EscCommand>();
            foreach (var command in root.Children)
            {
                if (!this.IsCommandConditionSatisfied(command))
                {
                    continue;
                }
                if (command.Children.Count > 0)
                {
                    foreach (var child in command.Children)
                    {
                        if (child.Children.Count > 0)
                        {
                            commandSequence.AddRange(this.GetCommandSequence(child));
                        }
                        else
                        {
                            commandSequence.Add(child);
                        }
                    }
                }
            }

            return commandSequence;
        }

        private bool IsCommandConditionSatisfied(EscCommand command)
        {
            foreach (var key in command.Conditions.Keys)
            {
                if (!this.globals.ContainsKey(key) && command.Conditions[key] == false ||
                    this.globals.ContainsKey(key) && this.globals[key] != command.Conditions[key])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
