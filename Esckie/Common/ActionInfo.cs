using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esckie.Common
{
    public class ActionInfo
    {
        public IList<Type> Parameters { get; set; }
        public Type ActionType { get; set; }
    }
}
