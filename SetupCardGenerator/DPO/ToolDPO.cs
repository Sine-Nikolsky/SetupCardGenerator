using System;

namespace SetupCardGenerator.DPO
{
    public class ToolDPO
    {
        public Guid SetId { get; set; }

        public Guid ToolSetupId { get; set; }

        public Guid SchemeId { get; set; }

        public Guid MyProperty { get; set; }

        public string SetName { get; set; }

        public string Machine { get; set; }

        public string ToolSetupName { get; set; }

        public string Outhand { get; set; }

        public string T_Num { get; set; }

        public string D_Num { get; set; }

        public byte[] Image { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Quantity { get; set; }

        public string PaddingLeft { get; set; }

        public string PaddingTop { get; set; }

        public ToolDPO()
        {
        }
    }
}
