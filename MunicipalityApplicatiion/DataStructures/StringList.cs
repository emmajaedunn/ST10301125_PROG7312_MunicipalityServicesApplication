using System;
using System.Collections.Generic;

namespace MunicipalityApplicatiion.DataStructures
{
    public sealed class StringList : List<string>
    {
        // Add a string only if it doesn't already exist
        public new void Add(string item)
        {
            if (!this.Contains(item))
                base.Add(item);
        }

        // Execute an action on every string
        public void ForEach(Action<string> action)
        {
            foreach (var item in this)
                action(item);
        }

        // Convert all items to a single string with separator
        public string ToDelimitedString(string separator = ", ")
        {
            return string.Join(separator, this);
        }
    }
}