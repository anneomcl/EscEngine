using Esckie.Common;
using Esckie.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Esckie
{
    public class EscController
    {
        private Dictionary<string, ActionMetadata> scriptActions;
        private EscVirtualMachine escVirtualMachine;

        public EscController(params Assembly[] executingAssemblies)
            : this(new Dictionary<string, bool>(), executingAssemblies)
        {
        }

        internal EscController(
            Dictionary<string, bool> globals,
            params Assembly[] executingAssemblies)
        {
            this.scriptActions = new Dictionary<string, ActionMetadata>();
            this.escVirtualMachine = new EscVirtualMachine(globals);

            var assemblies = executingAssemblies
                .SelectMany(x => x.GetTypes())
                .Where(t => t.IsSubclassOf(typeof(EscAction)) && t.IsClass)
                .Distinct()
                .ToList();

            foreach (var assembly in assemblies)
            {
                var actions = assembly.GetMethods()
                    .Where(m => m.DeclaringType == assembly)
                    .Distinct();

                foreach (var action in actions)
                {
                    if (!this.scriptActions.ContainsKey(action.Name))
                    {
                        this.scriptActions.Add(
                            action.Name,
                            new ActionMetadata
                            {
                                ActionType = assembly,
                                Parameters = action.GetParameters()
                                    .Select(y => y.ParameterType)
                                    .ToList()
                            });
                    }
                }
            }
        }

        /// <summary>
        /// This should be called to initialize any interactable that has a .esc script associated with it.
        /// </summary>
        /// <param name="path">The path of the interactable object's .esc script path.</param>
        /// <returns>An event table for the object.</returns>
        public Dictionary<string, EscEvent> Compile(string path)
        {
            var eventTable = new Dictionary<string, EscEvent>();
            var lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string eventName;
                if (EscCompilerHelpers.IsComment(lines[i]))
                {
                    continue;
                }
                else if (EscCompilerHelpers.TryParseEscEvent(lines[i], out eventName))
                {
                    var escEvent = EscEventFactory.Create(
                        eventName,
                        lines.Skip(i + 1).ToList(),
                        this.scriptActions);
                    eventTable.Add(escEvent.EventName, escEvent);
                }
            }

            return eventTable;
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

            var commands = this.escVirtualMachine.GetCommandSequence(eventTable[eventName].EventRoot);
            foreach (var task in commands)
            {
                var taskFunc = this.scriptActions[task.Name].ActionType.GetMethod(task.Name);
                taskFunc.Invoke(null, task.Parameters.ToArray());
            }
        }
    }
}
