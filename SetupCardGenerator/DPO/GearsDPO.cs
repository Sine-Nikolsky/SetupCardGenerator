using System;

namespace SetupCardGenerator.DPO
{
    public class GearsDPO
    {
        public Guid SetId { get; set; }

        public string SetName { get; set; }

        public string SetMachine { get; set; }

        public string ToolCode { get; set; }

        public string ToolName { get; set; }

        public string ToolType { get; set; }

        public int Quantity { get; set; }
    }
}
