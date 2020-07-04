using System;
using System.Collections.Generic;

namespace SetupCardGenerator
{
    [Serializable]
    public class Project
    {

        public string Author { get; set; }

        public string Detail { get; set; }

        public DateTime CreateDate { get; set; }

        public List<Set> Sets { get; set; }

        public Project()
        {
            CreateDate = DateTime.Now;
        }

    }
}
