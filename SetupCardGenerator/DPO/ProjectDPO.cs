using System;

namespace SetupCardGenerator.DPO
{
    public class ProjectDPO
    {
        public string Author { get; set; }

        public DateTime CreateDate { get; set; }

        public string Detail { get; set; }

        public double Time { get; set; }

        public ProjectDPO(Project p)
        {
            Author = p.Author;
            CreateDate = p.CreateDate;
            Detail = p.Detail;
            Time = 0;
            foreach (var i in p.Sets)
            {
                foreach (var j in i.Notes)
                    Time += j.Time;
            }
        }
    }
}
