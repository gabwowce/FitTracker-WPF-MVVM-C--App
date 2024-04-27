using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Models
{
    public class DayRecord
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public float? Weight { get; set; }
        public int? Calories { get; set; }
        public float? WaterIntake { get; set; }
        public float? SleepHours { get; set; }
        public string Notes { get; set; }
        public List<string> Activities { get; set; }
        public List<string> Moods { get; set; }
        public List<string> OtherFactors { get; set; }
    }
}
