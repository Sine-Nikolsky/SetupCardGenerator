using SetupCardGenerator.Model;
using System;

namespace SetupCardGenerator.DPO
{
    public class PartOfToolSetupDPO
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public PartOfToolSetupDPO()
        {
        }

        public PartOfToolSetupDPO(PartOfToolSetup p)
        {
            Id = p.Tool.Id;
            Type = p.Tool.Type;
            Code = p.Tool.Code;
            Name = p.Tool.Name;
            Count = p.Count;
        }
    }
}
