using System.Diagnostics;

namespace Esckie.Test.TestActions
{
    public class DialogueEscAction : EscAction
    {
        public static bool Say(string character, string text)
        {
            Debug.WriteLine($"{character}: {text}");
            return true;
        }
    }
}
