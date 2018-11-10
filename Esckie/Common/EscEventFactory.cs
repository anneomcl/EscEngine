using System;
using System.Collections.Generic;
using System.Linq;

namespace Esckie.Common
{
    public static class EscEventFactory
    {
        public static EscEvent Create(string eventName, List<string> lines, Dictionary<string, ActionMetadata> actions)
        {
            var escEvent = new EscEvent(eventName);

            var root = new EscCommand("Root");
            var currIndentLevel = 0;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var indentLevel = EscCompilerHelpers.GetIndentationLevel(line);
                if (indentLevel < currIndentLevel || EscCompilerHelpers.IsEventProcessingFinished(line))
                {
                    break;
                }
                else if (indentLevel > currIndentLevel)
                {
                    throw new Exception();
                }
                else
                {
                    root.Children.Add(EscCommandFactory.Create(line, currIndentLevel, actions));
                }
            }
            escEvent.EventRoot = root;

            return escEvent;
        }
    }
}
