using System;

namespace SetupCardGenerator.Model
{
    [Serializable]
    public class PartOfToolSetup
    {
        public Tool Tool { get; set; }

        public int Count { get; set; }
    }
}
