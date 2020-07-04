using System;
using System.Drawing;

namespace SetupCardGenerator.Model
{
    [Serializable]
    public class MyImage
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FullPath { get; set; }

        public Image Image { get; set; }

        public MyImage(string pathFile)
        {
            Id = Guid.NewGuid();
            Image = Image.FromFile(pathFile);
            Name = System.IO.Path.GetFileName(pathFile);
            FullPath = pathFile;
        }

        public override string ToString() => Name;
    }
}
