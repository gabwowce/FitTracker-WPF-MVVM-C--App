using FitTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using Org.BouncyCastle.Utilities;


namespace FitTracker.ViewModels
{
    internal class AddDayViewModel
    {
        public DateTime Date { get; set; } = DateTime.Today;
        private float? _weight; // Private field to hold the nullable weight value

        public float? Weight
        {
            get { return _weight; }
            set
            {
                if (_weight != value) // Check to prevent unnecessary updates
                {
                    _weight = value;
                    OnPropertyChanged(nameof(Weight));
                    CurrentRecord.Weight = _weight; // Update the CurrentRecord
                }
            }
        }
        public int? Calories { get; set; }
        public float? WaterIntake { get; set; }
        public float? SleepHours { get; set; }
        private string _note;
        public string Notes
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged(nameof(Notes));
                CurrentRecord.Notes = _note; // Atnaujinti CurrentRecord
            }
        }

        public ObservableCollection<string> Activities { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Moods { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> OtherFactors { get; set; } = new ObservableCollection<string>();

        public ICommand ToggleActivityCommand { get; }
        public ICommand ToggleMoodCommand { get; }
        public ICommand ToggleOtherFactorCommand { get; }





        private DayRecordRepository repository;
        public DayRecord CurrentRecord { get; set; }
        public ICommand SaveCommand { get; private set; }
        public AddDayViewModel(string connectionString)
        {
            ToggleActivityCommand = new RelayCommand<string>(ToggleActivity);
            ToggleMoodCommand = new RelayCommand<string>(ToggleMood);
            ToggleOtherFactorCommand = new RelayCommand<string>(ToggleOtherFactor);

            repository = new DayRecordRepository(connectionString);

            CurrentRecord = new DayRecord
            {
                Date = DateTime.Today,
                Activities = new List<string>(),
                Moods = new List<string>(),
                OtherFactors = new List<string>(),
                Weight = Weight,
                Calories = Calories,
                WaterIntake = WaterIntake,
                SleepHours = SleepHours,
                Notes = Notes
            };
           
        }

        private void ToggleActivity(string activity)
        {
            ToggleItem(Activities, activity);
        }

        private void ToggleMood(string mood)
        {
            ToggleItem(Moods, mood);
        }

        private void ToggleOtherFactor(string factor)
        {
            ToggleItem(OtherFactors, factor);
        }

        private void ToggleItem(ObservableCollection<string> collection, string item)
        {
            if (collection.Contains(item))
            {
                collection.Remove(item);
            }
            else
            {
                collection.Add(item);
            }
        }


        // Metodai, skirti pridėti ar pašalinti veiklas, nuotaikas ir kita.

        public void SaveRecord()
        {
            repository.AddDayRecord(CurrentRecord);
            OnPropertyChanged("CurrentRecord");
        } 

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

 }





