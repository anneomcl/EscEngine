using System.Collections.Generic;

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
        }

        /// <summary>
        /// This is called when an .esc script should be executed.
        /// </summary>
        public void RunEvent(Dictionary<string, EscEvent> eventTable, string eventName)
        {
            if (!eventTable.ContainsKey(eventName))
            {
                return;
            }

            var commands = eventTable[eventName].EventRoot.Children;
            foreach(var task in commands)
            {
                this.ExecuteAction(task);
            }
        }

        private void ExecuteAction(EscCommand task)
        {
            var taskFunc = EscActionsHandler.ScriptActions[task.Name].ActionType.GetMethod(task.Name);
            taskFunc.Invoke(null, task.Parameters.ToArray());
        }
    }
}
