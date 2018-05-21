using System;
using System.Collections.Generic;

namespace Esckie
{
    public class EscActionsBase : IEscActionsBase
    {
        protected static Dictionary<string, IList<Type>> ScriptActions =
            new Dictionary<string, IList<Type>>()
            {
                { "Say", new List<Type>() { typeof(string), typeof(string) } }
            };
    }
}
