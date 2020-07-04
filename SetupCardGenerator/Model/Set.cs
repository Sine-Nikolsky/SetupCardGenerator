using SetupCardGenerator.Model;
using System;
using System.Collections.Generic;

namespace SetupCardGenerator
{
    [Serializable]
    public class Set
    {
        public Guid Id { get; set; }

        public string SetName { get; set; }

        public string Machine { get; set; }

        public List<ToolSetup> ToolSetups { get; set; }

        public SetupScheme Scheme { get; set; }

        public List<Note> Notes { get; set; }

        public Set()
        {
            Id = Guid.NewGuid();
            Scheme = new SetupScheme();
            Notes = new List<Note>();
        }

        public Set(string name, string machine) : this()
        {
            SetName = name;
            Machine = machine;
        }

        public override string ToString()
        {
            return SetName + " (" + Machine + ")";
        }

    }
}
