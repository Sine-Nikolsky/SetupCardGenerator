using System;

namespace SetupCardGenerator
{
    [Serializable]
    public class Machine
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Machine(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
