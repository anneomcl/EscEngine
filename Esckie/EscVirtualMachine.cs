using System.Collections.Generic;
using System.Linq;

namespace Esckie
{
    public class EscVirtualMachine
    {
        private static EscVirtualMachine vm;
        public static EscVirtualMachine Instance
        {
            get
            {
                if (vm == null)
                {
                    vm = new EscVirtualMachine();
                }
                return vm;
            }
        }

        private EscVirtualMachine()
        {
            this.Globals = new Dictionary<string, bool>();
        }

        public Dictionary<string, bool> Globals { get; set; }

        /// <summary>
        /// This is called when an .esc script should be executed.
        /// </summary>
        public void RunEvent(Dictionary<string, EscEvent> eventTable, string eventName)
        {
            if (!eventTable.ContainsKey(eventName))
            {
                return;
            }

            var commands = this.GetCommandSequence(eventTable[eventName].EventRoot);
            foreach(var task in commands)
            {
                this.ExecuteAction(task);
            }
        }

        private List<EscCommand> GetCommandSequence(EscCommand root)
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
                if (!Globals.ContainsKey(key) && command.Conditions[key] == false ||
                    Globals.ContainsKey(key) && Globals[key] != command.Conditions[key])
                {
                    return false;
                }
            }

            return true;
        }

        private void ExecuteAction(EscCommand task)
        {
            var taskFunc = EscActionsHandler.ScriptActions[task.Name].ActionType.GetMethod(task.Name);
            taskFunc.Invoke(null, task.Parameters.ToArray());
        }
    }
}
