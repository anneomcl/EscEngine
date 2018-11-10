using System;
using System.Collections.Generic;
using System.Linq;
using Esckie.Common;

namespace Esckie
{
    public class EscEvent
    {
        public EscEvent(string eventName)
        {
            this.EventName = eventName;
        }

        public string EventName { get; set; }

        public EscCommand EventRoot { get; set; }
    }
}