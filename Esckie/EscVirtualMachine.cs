using Esckie.Actions;

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
        /// <remarks>Call this on a player input for an event.
        /// For example, on an "Interactable", call this when the
        /// "interact" input is given. Need to compile script attached
        /// to object into event table, then pass to this.</remarks>
        public void RunEvents(EscEventTable escEventTable, EscActionsHandler actionHandler, string eventName)
        {
            var table = escEventTable.eventTable;
            if (!table.ContainsKey(eventName))
            {
                return;
            }

            var commands = table[eventName].EventRoot.Links;
            foreach(var task in commands)
            {
                actionHandler.ExecuteAction(task);
            }
        }
    }
}
