using SetupCardGenerator.Model;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SetupCardGenerator
{
    [Serializable]
    public class ToolSetup
    {
        public Guid Id { get; set; }

        public List<PartOfToolSetup> Tools { get; set; }

        public string Name { get; set; }

        public int OutHand { get; set; }

        public int Num_T { get; set; }

        public int Num_D { get; set; }

        public Image Image { get; set; }

        public ToolSetup()
        {
            Id = Guid.NewGuid();

        }

        public ToolSetup(ToolSetup clone) : this()
        {
            Tools = clone.Tools;
            Name = clone.Name;
            OutHand = clone.OutHand;
            Num_D = clone.Num_D;
            Num_T = clone.Num_T;
            Image = clone.Image;
        }

        public override string ToString()
        {
            return string.Format($"<T{Num_T} D{Num_D}> {Name}");
        }
    }
}
