using System;

namespace SetupCardGenerator.DPO
{
    public class NotesDPO
    {
        public Guid SetId { get; set; }

        public string SetName { get; set; }

        public string SetMachine { get; set; }

        public Guid Id { get; set; }

        public int Num { get; set; }

        public string Description { get; set; }

        public string ToolNum { get; set; }

        public int Coeff { get; set; }

        /// <summary>
        /// Время обработки, мин
        /// </summary>
        public double Time { get; set; }

        public double FullTime { get; set; }
    }
}
