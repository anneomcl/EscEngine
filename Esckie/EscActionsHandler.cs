using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Esckie.Common;

namespace Esckie
{
    public class EscActionsHandler
    {
        public Dictionary<string, ActionInfo> ScriptActions { get; }

        public EscActionsHandler()
        {
            this.ScriptActions = new Dictionary<string, ActionInfo>();
            this.InitializeActions(Assembly.GetExecutingAssembly());
        }

        public EscActionsHandler(Assembly currAssembly)
        {
            this.ScriptActions = new Dictionary<string, ActionInfo>();
            this.InitializeActions(currAssembly);
        }

        public void ExecuteAction(VmCommand task)
        {
            var taskFunc = ScriptActions[task.Name].ActionType.GetMethod(task.Name);
            taskFunc.Invoke(null, task.Parameters);
        }

        private void InitializeActions(Assembly currAssembly)
        {
            var assemblies = currAssembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(EscActions)) && t.IsClass)
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
                            new ActionInfo
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
