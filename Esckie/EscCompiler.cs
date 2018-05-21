using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Esckie
{
    public class EscCompiler
    {
        protected static EscCompiler _compiler;
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

        protected EscCompiler()
        {
        }

        public EscEventTable Compile(string path, Dictionary<string, IList<Type>> actions)
        {
            var eventTable = new EscEventTable();
            var lines = File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
            {
                EscEvent escEvent;
                if (EscCompilerHelpers.IsComment(lines[i]))
                {
                    continue;
                }
                else if (EscCompilerHelpers.TryParseEscEvent(lines[i], out escEvent))
                {
                    escEvent.CompileEscEvent(lines.ToList(), i + 1, actions);
                    eventTable.AddEvent(escEvent.EventName, escEvent);
                }
            }

            return eventTable;
        }
    }
}
