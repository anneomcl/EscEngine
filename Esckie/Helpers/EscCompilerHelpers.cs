using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Esckie.Common;

namespace Esckie
{
    public static class EscCompilerHelpers
    {
        public const string EventIndicator = ":";
        public const string CommentIndicator = "#";

        /// <remarks>
        /// A line is either a comment or not a comment.
        /// Comments at the end of lines are not supported.
        /// </remarks>
        public static bool IsComment(string line)
        {
            if (!string.IsNullOrWhiteSpace(line) &&
                line.First() == CommentIndicator.First())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Parses a line of Esckie to a list of string tokens.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static IList<string> ParseLineToTokens(string line)
        {
            return Regex.Matches(line, "[^\\s\"']+|\"[^\"]*\"|'[^']*'")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
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

        /// <summary>
        /// Cleans and parses an event name from a string.
        /// </summary>
        /// <remarks>
        /// For example, starting a script with :interact means that the
        /// script will be executed when an "interact" event occurs on the object.
        /// </remarks>
        public static bool TryParseEscEvent(string line, out string eventName)
        {
            if (!string.IsNullOrWhiteSpace(line) &&
                line.First() == EventIndicator.First())
            {
                eventName = line.Substring(1);
                return true;
            }
            else
            {
                eventName = null;
                return false;
            }
        }
    }
}
