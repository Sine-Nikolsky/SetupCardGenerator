using System;
using System.Collections.Generic;

namespace SetupCardGenerator.Model
{
    [Serializable]
    public class SetupScheme
    {
        public Guid Id { get; set; }

        public List<MyImage> Images { get; set; }

        public List<PartOfToolSetup> Gears { get; set; }

        public string Note { get; set; }

        public SetupScheme()
        {
            Id = Guid.NewGuid();
            Images = new List<MyImage>();
            Gears = new List<PartOfToolSetup>();
        }
    }
}
