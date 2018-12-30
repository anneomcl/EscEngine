using System;
using System.Collections.Generic;
using System.Linq;
using Esckie.Helpers;

namespace Esckie.Common
{
    public static class EscEventFactory
    {
        public static class Keywords
        {
            public static string If => "if";
            public static string Else => "else";
            public static string ElseIf => "elif";
        };

        public static EscEvent Create(string eventName, List<string> lines, Dictionary<string, ActionMetadata> actions)
        {
            var escEvent = new EscEvent(eventName);

            var root = ConvertScriptToLogicTree(0, lines, new EscCommand("root"), actions);
            escEvent.EventRoot = root;

            return escEvent;
        }

        private static EscCommand ConvertScriptToLogicTree(int rootIndentLevel, List<string> lines, EscCommand root, Dictionary<string, ActionMetadata> actions)
        {
            // Base cases: 1) end of file, 2) child block returning to parent, or 3) end of current event.
            if (lines.Count == 0 ||
                EscCompilerHelpers.GetIndentationLevel(lines.First()) < rootIndentLevel ||
                EscCompilerHelpers.IsEventProcessingFinished(lines.First()))
            {
                return root;
            }

            // Validate the line is parseable
            var line = lines.First();
            var indentLevel = EscCompilerHelpers.GetIndentationLevel(line);
            if (!ValidateSyntax(line, lines))
            {
                return ConvertScriptToLogicTree(indentLevel, lines, root, actions);
            }

            // Process child blocks
            var tokens = EscCompilerHelpers.ParseLineToTokens(line);
            var action = tokens.First();
            if (action == Keywords.If || action == Keywords.Else || action == Keywords.ElseIf)
            {
                ProcessControlLogic(action, tokens, indentLevel, lines, root, actions);
            }

            // Process custom actions
            if (actions.Keys.Contains(action))
            {
                root.Children.Add(EscCommandFactory.Create(tokens, actions));
                lines.RemoveAt(0);
                return ConvertScriptToLogicTree(indentLevel, lines, root, actions);
            }
            else
            {
                throw new InvalidOperationException($"There is no action {action} available.");
            }
        }

        private static KeyValuePair<string, bool> ParseCondition(string condition)
        {
            if (condition[0] == '!')
            {
                return new KeyValuePair<string, bool>(condition.Substring(1), false);
            }
            else
            {
                return new KeyValuePair<string, bool>(condition, true);
            }
        }

        private static void ProcessControlLogic(string action, List<string> tokens, int indentLevel, List<string> lines, EscCommand root, Dictionary<string, ActionMetadata> actions)
        {
            if (action == Keywords.If)
            {
                var condition = ParseCondition(tokens[1]);

                var currRoot = new EscCommand()
                {
                    Name = Keywords.If,
                    Conditions = new Dictionary<string, bool>() { { condition.Key, condition.Value } },
                    Children = new List<EscCommand>()
                };

                lines.RemoveAt(0);
                root.Children.Add(ConvertScriptToLogicTree(indentLevel + 1, lines, currRoot, actions));
            }
            else if (action == Keywords.Else)
            {
                if (root.Children.Last().Name != Keywords.If)
                {
                    throw new InvalidOperationException("No valid 'if' block paired with 'else'.");
                }

                var conditions = root.Children.Last().Conditions.Select(x => new KeyValuePair<string, bool>(x.Key, !x.Value)).ToDictionary(x => x.Key, x => x.Value);
                var currRoot = new EscCommand()
                {
                    Name = Keywords.Else,
                    Conditions = conditions,
                    Children = new List<EscCommand>()
                };

                lines.RemoveAt(0);
                root.Children.Add(ConvertScriptToLogicTree(indentLevel + 1, lines, currRoot, actions));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static bool ValidateSyntax(string line, List<string> lines)
        {
            if (StringExtensions.IsNullOrWhiteSpace(line))
            {
                lines.RemoveAt(0);
                return false;
            }

            return true;
        }
    }
}
