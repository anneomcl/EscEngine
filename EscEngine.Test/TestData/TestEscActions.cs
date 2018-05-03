using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscEngine.Test.TestData
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
