using System;

namespace SetupCardGenerator
{
    [Serializable]
    public class Tool
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public Tool(string code, string name, string type)
        {
            Code = code;
            Name = name;
            Type = type;
            Id = Guid.NewGuid();
        }
        public Tool(Tool cl)
        {
            Id = Guid.NewGuid();
            Code = cl.Code;
            Name = cl.Name;
            Type = cl.Type;
        }

        public override string ToString()
        {
            return string.Format($"({Type}) {Name}");
        }
    }
}
