using FitTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using FitTracker.Commands;
using Google.Protobuf;


namespace FitTracker.ViewModels
{
    internal class AddDayViewModel : INotifyPropertyChanged
    {
        public string Date { get; set; } /*= DateTime.Now.ToString("yyyy-MM-dd");*/
        private float? _weight;
        private int? _calories;
        private float? _waterIntake;
        private float? _sleepHours;
        private string _notes;

        public float? Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged(nameof(Weight));
                    CurrentRecord.Weight = _weight;
                }
            }
        }
        public int? Calories
        {
            get => _calories;
            set
            {
                if (_calories != value)
                {
                    _calories = value;
                    OnPropertyChanged(nameof(Calories));
                    CurrentRecord.Calories = _calories;
                }
            }
        }
        public float? WaterIntake
        {

            get => _waterIntake;
            set
            {
                if (_waterIntake != value)
                {
                    _waterIntake = value;
                    OnPropertyChanged(nameof(WaterIntake));
                    CurrentRecord.WaterIntake = _waterIntake;
                }
            }
        }
        public float? SleepHours
        {

            get => _sleepHours;
            set
            {
                if (_sleepHours != value)
                {
                    _sleepHours = value;
                    OnPropertyChanged(nameof(SleepHours));
                    CurrentRecord.SleepHours = _sleepHours;
                }
            }
        }
        public string Notes
        {
            get => _notes;
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged(nameof(Notes));
                    CurrentRecord.Notes = _notes;
                }
            }
        }

        public ObservableCollection<string> Activities { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Moods { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> OtherFactors { get; } = new ObservableCollection<string>();

        public ICommand ToggleActivityCommand { get; }
        public ICommand ToggleMoodCommand { get; }
        public ICommand ToggleOtherFactorCommand { get; }

        private DayRecordRepository repository;
        public DayRecord CurrentRecord { get; private set; }

        public AddDayViewModel(string connectionString, string dateString)
        {
            repository = new DayRecordRepository(connectionString);

            DateTime parsedDate;
            if (DateTime.TryParse(dateString, out parsedDate))
            {
                this.Date = parsedDate.ToString("MMMM dd"); // Saugo norimą formatą rodyti vartotojui.
                CurrentRecord = new DayRecord
                {
                    Date = parsedDate, // Čia saugo DateTime objektą, kuris eis į DB.
                    Activities = new List<string>(),
                    Moods = new List<string>(),
                    OtherFactors = new List<string>()
                };
            }

            ToggleActivityCommand = new ActionCommand(activity => ToggleItem(Activities, activity));
            ToggleMoodCommand = new ActionCommand(mood => ToggleItem(Moods, mood));
            ToggleOtherFactorCommand = new ActionCommand(factor => ToggleItem(OtherFactors, factor));
        }




        private void ToggleItem(ObservableCollection<string> collection, string item)
        {
            if (collection != null && !string.IsNullOrEmpty(item))
            {
                // Čia debaginimo eilutė, kuri išves item reikšmę į Output langą
                System.Diagnostics.Debug.WriteLine("Toggled item: " + item);

                if (collection.Contains(item))
                {
                    collection.Remove(item);
                    System.Diagnostics.Debug.WriteLine("Removed from collection");
                }
                else
                {
                    collection.Add(item);
                    System.Diagnostics.Debug.WriteLine("Added to collection");
                }
            }
        }



        // Metodai, skirti pridėti ar pašalinti veiklas, nuotaikas ir kita.

        public void SaveRecord()
        {
            CurrentRecord.Weight = this.Weight;
            CurrentRecord.Calories = this.Calories;
            CurrentRecord.WaterIntake = this.WaterIntake;
            CurrentRecord.SleepHours = this.SleepHours;
            CurrentRecord.Notes = this.Notes;


            CurrentRecord.Activities = new List<string>(Activities);
            CurrentRecord.Moods = new List<string>(Moods);
            CurrentRecord.OtherFactors = new List<string>(OtherFactors);

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





