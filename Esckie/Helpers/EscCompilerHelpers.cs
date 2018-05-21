using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Esckie
{
    public static class EscCompilerHelpers
    {
        private const string EventIndicator = ":";

        public static bool IsComment(string line)
        {
            foreach (char i in line)
            {
                if (i == '#')
                {
                    return true;
                }
                else if (!String.IsNullOrWhiteSpace(i.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        public static IList<string> ParseLineToTokens(string line)
        {
            return Regex.Matches(line, "[^\\s\"']+|\"[^\"]*\"|'[^']*'")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
        }

        public static bool IsConditionFlag(string line)
        {
            if (line[0] == '[' && line[line.Length - 1] == ']')
            {
                return true;
            }

            return false;
        }

        public static bool IsEventProcessingFinished(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return false;
            }

            return line.First() == EventIndicator.First();
        }

        public static int GetIndentationLevel(string line)
        {
            var lineArray = line.ToCharArray();
            for (int i = 0; i < line.Length; i++)
            {
                if (lineArray[i] != '\t')
                {
                    return i;
                }
            }

            return 0;
        }

        public static string Trim(string line)
        {
            return new string(line.Where(x => !char.IsWhiteSpace(x)).ToArray());
        }

        /// <summary>
        /// Cleans and parses an event from a string.
        /// </summary>
        /// <remarks>
        /// For example, starting a script with :interact means that the
        /// script will be executed when an "interact" event occurs on the object.
        /// </remarks>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool TryParseEscEvent(string line, out EscEvent ev)
        {
            var cleanLine = EscCompilerHelpers.Trim(line);
            if (cleanLine.First() == EventIndicator.First())
            {
                ev = new EscEvent()
                {
                    EventName = cleanLine.Replace(EventIndicator, "")
                };
                return true;
            }

            ev = new EscEvent();
            return false;
        }
    }
}
