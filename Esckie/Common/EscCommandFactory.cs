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
        public static EscCommand Create(string line, int indentLevel, Dictionary<string, ActionMetadata> actions)
        {
            var newCommand = new EscCommand();

            var tokens = EscCompilerHelpers.ParseLineToTokens(line);

            //Ensure action is valid, and set the root's action
            if (!actions.Keys.Contains(tokens.First()))
            {
                throw new Exception();
            }

            newCommand.Name = tokens.First();
            tokens.RemoveAt(0);

            //Set the root's parameters for the given action
            if (tokens.Count() != actions[newCommand.Name].Parameters.Count())
            {
                throw new Exception();
            }

            newCommand.Parameters = tokens;

            return newCommand;
        }
    }
}
