using FitTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media;
using static FitTracker.ViewModels.MenuBarViewModel;

namespace FitTracker.ViewModels
{
    public class DayInfo
    {
        public string Day { get; set; }
        public string MonthDay { get; set; }
        public Brush BackgroundColor { get; set; }
        public string WeekDay { get; set; }

        public Brush FontColor { get; set; }
        public string TodayDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");


    }


    public class MyDiaryPageViewModel : INotifyPropertyChanged

    {
        private ObservableCollection<DayRecord> _dayRecords;
        public ObservableCollection<DayRecord> DayRecords
        {
            get => _dayRecords;
            set
            {
                _dayRecords = value;
                OnPropertyChanged(nameof(DayRecords));
            }
        }

        public async Task InitializeAsync()
        {
            await LoadDayRecords();
        }

        private async Task LoadDayRecords()
        {
            var repository = new DayRecordRepository(ConfigurationManager.ConnectionStrings["MyConnectionToDB"].ConnectionString);
            var dayRecords = await repository.GetDayRecordsForUserAsync(UserSession.UserId);
            DayRecords = new ObservableCollection<DayRecord>(dayRecords);
            OnPropertyChanged(nameof(DayRecords));
        }

        public void DeleteDayRecord(DayRecord record)
        {
            if (record != null)
            {
                var repository = new DayRecordRepository(ConfigurationManager.ConnectionStrings["MyConnectionToDB"].ConnectionString);
                repository.DeleteDayRecord(record.LogID);
                DayRecords.Remove(record);
            }
        }


        public string TodayDate { get; set; }
        private string _currentMonthYear;
        public string CurrentMonthYear
        {
            get => _currentMonthYear;
            private set
            {
                if (_currentMonthYear != value)
                {
                    _currentMonthYear = value;
                    OnPropertyChanged();
                }
            }
        }
        public MenuBarViewModel MenuBar { get; private set; }
        public Brush DiaryButtonBackground => MenuBar.DiaryButtonBackground;

        private List<DayInfo> _weekDays;
        public List<DayInfo> WeekDays
        {
            get => _weekDays;
            set
            {
                _weekDays = value;
                OnPropertyChanged();
            }
        }

        public MyDiaryPageViewModel(MenuBarViewModel menuBarViewModel)
        {
            
            MenuBar = menuBarViewModel;
            CurrentMonthYear = DateTime.Now.ToString("yyyy MMMM");
            TodayDate = DateTime.Now.ToString("yyyy-MM-dd");
            GenerateWeekDays();

        }


        private void GenerateWeekDays()
        {
            SolidColorBrush myCustomColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFB2FF"));
            SolidColorBrush greyBrush = new SolidColorBrush(Color.FromArgb(255, 121, 121, 121)); // Pilka spalva
            WeekDays = new List<DayInfo>();
            var today = DateTime.Today;
            // Koreguojame pradžią, kad savaitė prasidėtų nuo pirmadienio
            var correction = today.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)today.DayOfWeek - 1;
            var startOfWeek = today.AddDays(-correction - 2); // Anksčiau buvo -(int)today.DayOfWeek - 2
            string[] weekdayAbbreviations = { "S", "M", "T", "W", "T", "F", "S" };

            for (int i = 0; i < 11; i++) // Generuojama 11 dienų
            {
                var date = startOfWeek.AddDays(i);
                bool isBeforeOrAfterWeek = i < 2 || i > 8; // Dvi dienos prieš ir dvi po savaitės
                var dayInfo = new DayInfo
                {
                    Day = date.ToString("dd"),
                    BackgroundColor = (date == today) ? myCustomColor : Brushes.White,
                    FontColor = isBeforeOrAfterWeek ? greyBrush : Brushes.Black,
                    WeekDay = (date == today) ? "Today" : weekdayAbbreviations[(int)date.DayOfWeek],
                    MonthDay = date.ToString("MMMM dd")
                };
                WeekDays.Add(dayInfo);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
