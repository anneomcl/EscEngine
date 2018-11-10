using System.Collections.Generic;
using System.IO;
using System.Linq;
using Esckie.Common;
using Esckie.Helpers;

namespace Esckie
{
    public class EscCompiler
    {
        private static EscCompiler _compiler;
        public static EscCompiler Instance
        {
            get
            {
                if (_compiler == null)
                {
                    _compiler = new EscCompiler();
                }
                return _compiler;
            }
        }

        private EscCompiler()
        {
        }

        public Dictionary<string, EscEvent> Compile(string path, Dictionary<string, ActionMetadata> actions)
        {
            var eventTable = new Dictionary<string, EscEvent>();
            var lines = File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
            {
                string eventName;
                if (EscCompilerHelpers.IsComment(lines[i]))
                {
                    continue;
                }
                else if (EscCompilerHelpers.TryParseEscEvent(lines[i], out eventName))
                {
                    var escEvent = EscEventFactory.Create(eventName, lines.Skip(i + 1).ToList(), actions);
                    eventTable.Add(escEvent.EventName, escEvent);
                }
            }

            return eventTable;
        }
    }
}
