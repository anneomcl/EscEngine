using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EscEngine
{
    public class EscCompiler
    {
        public static string EventIndicator => ":";

        public static EscEventTable Compile(string path, Dictionary<string, IList<Type>> actions)
        {
            var eventTable = new EscEventTable();
            var lines = File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
            {
                EscEvent escEvent;
                if (IsComment(lines[i]))
                {
                    continue;
                }
                else if (TryParseEvent(lines[i], out escEvent))
                {
                    escEvent.EventRoot = CompileEscEvent(lines.ToList(), i + 1, actions);
                    eventTable.AddEvent(escEvent.EventName, escEvent);
                }
            }

            return eventTable;
        }

        private static bool IsComment(string line)
        {
            foreach(char i in line)
            {
                if(i == '#')
                {
                    return true;
                }
                else if(!String.IsNullOrWhiteSpace(i.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Cleans and parses an event line.
        /// </summary>
        /// <remarks>
        /// For example, starting a script with :interact means that the
        /// script will be executed when an "interact" event occurs on the object.
        /// </remarks>
        /// <param name="line"></param>
        /// <returns></returns>
        private static bool TryParseEvent(string line, out EscEvent ev)
        {
            var cleanLine = Trim(line);
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

        private static VmCommand CompileEscEvent(List<string> lines, int startIndex, Dictionary<string, IList<Type>> actions)
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

                var indentLevel = GetIndentationLevel(line);
                if (indentLevel < currIndentLevel || EventProcessingFinished(line))
                {
                    break;
                }
                else if (indentLevel > currIndentLevel)
                {
                    throw new Exception();
                }
                else
                {
                    ProcessVmCommand(line, root, currIndentLevel, actions);
                }
            }

            return root;
        }

        private static void ProcessVmCommand(string line, VmCommand root, int indentLevel, Dictionary<string, IList<Type>> actions)
        {
            var newCommand = new VmCommand();

            var tokens = ParseLineToTokens(line);

            //Handle tokens that are condition flags

            //Ensure action is valid, and set the root's action
            if (!actions.Keys.Contains(tokens.First()))
            {
                throw new Exception();
            }

            newCommand.Name = tokens.First();
            tokens.RemoveAt(0);

            //Handle unary operators ">", "<", "?"
            //Handle dialog options

            //Set the root's parameters for the given action
            if (tokens.Count() != actions[newCommand.Name].Count())
            {
                throw new Exception();
            }

            newCommand.Parameters = tokens.ToArray();

            root.Links.Add(newCommand);
        }

        private static IList<string> ParseLineToTokens(string line)
        {
            return Regex.Matches(line, "[^\\s\"']+|\"[^\"]*\"|'[^']*'")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
        }

        private static bool IsConditionFlag(string line)
        {
            if (line[0] == '[' && line[line.Length - 1] == ']')
            {
                return true;
            }

            return false;
        }

        private static bool EventProcessingFinished(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return false;
            }

            return line.First() == EventIndicator.First();
        }

        private static int GetIndentationLevel(string line)
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
    }
}
