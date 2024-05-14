using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Models
{
    public class DayRecord : INotifyPropertyChanged
    {
        private DateTime _date;
        private float? _weight;
        private int? _calories;
        private float? _waterIntake;
        private float? _sleepHours;

        public int ID { get; set; }
        public int LogID { get; set; }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public float? Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        public int? Calories
        {
            get => _calories;
            set
            {
                _calories = value;
                OnPropertyChanged(nameof(Calories));
            }
        }

        public float? WaterIntake
        {
            get => _waterIntake;
            set
            {
                _waterIntake = value;
                OnPropertyChanged(nameof(WaterIntake));
            }
        }

        public float? SleepHours
        {
            get => _sleepHours;
            set
            {
                _sleepHours = value;
                OnPropertyChanged(nameof(SleepHours));
            }
        }

        public string Notes { get; set; }
        public List<string> Activities { get; set; }
        public List<string> Moods { get; set; }
        public List<string> OtherFactors { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
