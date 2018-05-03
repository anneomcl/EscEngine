﻿using System.Collections.Generic;

namespace EscEngine
{
    public class EscEventTable
    {
        public Dictionary<string, EscEvent> eventTable = new Dictionary<string, EscEvent>();

        public void AddEvent(string id, EscEvent ev)
        {
            eventTable.Add(id, ev);
        }
    }
}
