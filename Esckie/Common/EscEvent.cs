using System;
using System.Collections.Generic;
using System.Linq;
using Esckie.Common;

namespace Esckie
{
    public class EscEvent
    {
        public string EventName { get; set; }

        public VmCommand EventRoot { get; set; }

        /// <summary>
        /// Compiles an event's body to a tree of VM commands.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="startIndex"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public void CompileEscEvent(List<string> lines, int startIndex, Dictionary<string, ActionInfo> actions)
        {
            var root = new VmCommand();
            var eventLines = lines.GetRange(startIndex, lines.Count() - startIndex);
            var currIndentLevel = 0;
            foreach (var line in eventLines)
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
                    root.ProcessVmCommand(line, currIndentLevel, actions);
                }
            }

            this.EventRoot = root;
        }
    }
}