using System;

namespace SetupCardGenerator.DPO
{
    public class SchemeDPO
    {
        public Guid SetId { get; set; }

        public Guid SchemeId { get; set; }

        public string SetName { get; set; }

        public string SetMachine { get; set; }

        public string Note { get; set; }

        public byte[] Image { get; set; }

        public string ImageName { get; set; }

        public string ImageNote { get; set; }

        public string PaddingLeft { get; set; }

        public string PaddingTop { get; set; }

        public SchemeDPO()
        {
            ImageName = "";
        }
    }
}
