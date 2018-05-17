using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esckie
{
    public interface IEscActions : IEscActionsBase
    {
        bool Say(string character, string text);
    }
}
