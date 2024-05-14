using FitTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.ViewModels
{
    internal class DailyStatsViewModel : INotifyPropertyChanged
    {
        private DayRecord _dayRecord;
        public string Weight => _dayRecord.Weight.HasValue ? $"{_dayRecord.Weight.Value} kg" : "";
        public string Calories => _dayRecord.Calories.HasValue ? $"{_dayRecord.Calories.Value} kcal" : "";
        public string WaterIntake => _dayRecord.WaterIntake.HasValue ? $"{_dayRecord.WaterIntake.Value} L" : "";
        public string SleepHours => _dayRecord.SleepHours.HasValue ? $"{_dayRecord.SleepHours.Value} h" : "";
        public DateTime Date => _dayRecord.Date;

        public DailyStatsViewModel(DayRecord dayRecord)
        {
            _dayRecord = dayRecord;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
