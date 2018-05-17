using System;
using System.Collections.Generic;

namespace Esckie.Test.TestData
{
    public static class TestEscActions
    {
        public static Dictionary<string, IList<Type>> ScriptActions =
            new Dictionary<string, IList<Type>>()
            {
                { "Say", new List<Type>() { typeof(string), typeof(string) } }
            };

        public static bool Say(string character, string text)
        {
            return true;
        }
    }
}
