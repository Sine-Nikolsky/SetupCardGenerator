using System;

namespace SetupCardGenerator
{
    [Serializable]
    public class Note
    {
        public Guid Id { get; set; }
        public int Num { get; set; }

        public string Description { get; set; }

        public ToolSetup Tool { get; set; }

        /// <summary>
        /// Коэффициент стойкости
        /// </summary>
        public int Coeff { get; set; }

        /// <summary>
        /// Время обработки, мин
        /// </summary>
        public double Time { get; set; }

        public Note()
        {
            Id = Guid.NewGuid();
        }


    }
}
