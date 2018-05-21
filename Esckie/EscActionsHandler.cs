using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Esckie.Common;

namespace Esckie.Actions
{
    public class EscActionsHandler
    {
        public Dictionary<string, ActionInfo> ScriptActions { get; }

        private static EscActionsHandler _handler;
        public static EscActionsHandler Instance
        {
            get
            {
                if (_handler == null)
                {
                    _handler = new EscActionsHandler();
                }
                return _handler;
            }
        }

        private EscActionsHandler()
        {
            this.ScriptActions = new Dictionary<string, ActionInfo>();
            this.InitializeActions();
        }

        public void ExecuteAction(VmCommand task)
        {
            var taskFunc = ScriptActions[task.Name].ActionType.GetMethod(task.Name);
            taskFunc.Invoke(null, task.Parameters);
        }

        private void InitializeActions()
        {
            var assemblies = Assembly.GetExecutingAssembly().GetTypes()
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
