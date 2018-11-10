using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Esckie.Common;

namespace Esckie
{
    public class EscActionsHandler
    {
        public Dictionary<string, ActionMetadata> ScriptActions { get; }

        public EscActionsHandler(Assembly currAssembly)
        {
            this.ScriptActions = new Dictionary<string, ActionMetadata>();
            this.InitializeActions(currAssembly);
        }

        public void ExecuteAction(EscCommand task)
        {
            var taskFunc = ScriptActions[task.Name].ActionType.GetMethod(task.Name);
            taskFunc.Invoke(null, task.Parameters);
        }

        private void InitializeActions(Assembly currAssembly)
        {
            var assemblies = currAssembly.GetTypes()
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
                    if (!ScriptActions.ContainsKey(action.Name))
                    {
                        ScriptActions.Add(
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
    }
}
