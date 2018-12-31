using System;
using System.Collections.Generic;
using System.Linq;
using Esckie.Helpers;

namespace Esckie.Common
{
    public static class EscCommandFactory
    {
        /// <summary>
        /// Creates a new VM command and assigns it to the correct parent.
        /// The current node is the default parent reference.
        /// </summary>
        public static EscCommand Create(List<string> tokens, Dictionary<string, ActionMetadata> actions)
        {
            var newCommand = new EscCommand();

            newCommand.Name = tokens.First();
            tokens.RemoveAt(0);

            //Set the root's parameters for the given action
            if (tokens.Count != actions[newCommand.Name].Parameters.Count)
            {
                throw new InvalidOperationException($"Parameter count {tokens.Count} not equal to expected count {actions[newCommand.Name].Parameters.Count}.");
            }

            /* TO-DO: Validate types when values besides string are required.
            for (int i = 0; i < tokens.Count; i++)
            {
                if (!actions[newCommand.Name].Parameters[i].Parse(tokens[i]))
                {
                    throw new InvalidOperationException($"Parameter {tokens[i]} type not convertable to expected type {actions[newCommand.Name].Parameters[i]}");
                }
            }
            */

            newCommand.Parameters = tokens;

            return newCommand;
        }
    }
}
