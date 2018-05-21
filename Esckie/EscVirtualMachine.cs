using System;

namespace Esckie
{
    public static class EscVirtualMachine
    {
        /// <summary>
        /// This is called when an .esc script should be executed.
        /// </summary>
        /// <remarks>Call this on a player input for an event.
        /// For example, on an "Interactable", call this when the
        /// "interact" input is given. Need to compile script attached
        /// to object into event table, then pass to this.</remarks>
        public static void RunEvents(EscEventTable escEventTable, string eventName, Type actionType)
        {
            var table = escEventTable.eventTable;
            if (!table.ContainsKey(eventName))
            {
                return;
            }

            var commands = table[eventName].EventRoot.Links;
            foreach(var task in commands)
            {
                ProcessTask(task, actionType);
            }
        }

        private static void ProcessTask(VmCommand task, Type actionType)
        {
            var taskFunc = actionType.GetMethod(task.Name);
            taskFunc.Invoke(null, task.Parameters);
        }
    }
}
