using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Esckie.Common;

namespace Esckie
{
    public static class EscActionsHandler
    {
        public static Dictionary<string, ActionMetadata> ScriptActions = ScriptActions ?? new Dictionary<string, ActionMetadata>();

        public static void Initialize(Assembly executingAssembly)
        {
            var assemblies = executingAssembly.GetTypes()
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
