using System;
using System.Collections.Generic;

namespace Esckie.Common
{
    public class ActionMetadata
    {
        /// <summary>
        /// The parameter type requirements for an action.
        /// </summary>
        public IList<Type> Parameters { get; set; }

        /// <summary>
        /// The type of <see cref="EscAction"/>
        /// </summary>
        public Type ActionType { get; set; }
    }
}
