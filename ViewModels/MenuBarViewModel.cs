using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FitTracker.ViewModels
{
    public class MenuBarViewModel : INotifyPropertyChanged
    {
        private bool _isDiaryActive;
        private bool _isProfileActive;
        private bool _isDashboardActive;

        public enum ActivePage
        {
            Dashboard,
            Diary,
            Profile
        }

        public void SetActivePage(ActivePage activePage)
        {
            IsDashboardActive = activePage == ActivePage.Dashboard;
            IsDiaryActive = activePage == ActivePage.Diary;
            IsProfileActive = activePage == ActivePage.Profile;
        }

        public bool IsDiaryActive
        {
            get => _isDiaryActive;
            set
            {
                if (SetField(ref _isDiaryActive, value))
                {
                    OnPropertyChanged(nameof(DiaryButtonBackground));
                    OnPropertyChanged(nameof(ProfileButtonBackground));
                    OnPropertyChanged(nameof(DashboardButtonBackground));
                }
            }
        }

        public bool IsProfileActive
        {
            get => _isProfileActive;
            set
            {
                if (SetField(ref _isProfileActive, value))
                {
                    OnPropertyChanged(nameof(DiaryButtonBackground));
                    OnPropertyChanged(nameof(ProfileButtonBackground));
                    OnPropertyChanged(nameof(DashboardButtonBackground));
                }
            }
        }

        public bool IsDashboardActive
        {
            get => _isDashboardActive;
            set
            {
                if (SetField(ref _isDashboardActive, value))
                {
                    OnPropertyChanged(nameof(DiaryButtonBackground));
                    OnPropertyChanged(nameof(ProfileButtonBackground));
                    OnPropertyChanged(nameof(DashboardButtonBackground));
                }
            }
        }


        public Brush DiaryButtonBackground => IsDiaryActive ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFB2FF")) : Brushes.White;
        public Brush ProfileButtonBackground => IsProfileActive ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFB2FF")) : Brushes.White;
        public Brush DashboardButtonBackground => IsDashboardActive ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFB2FF")) : Brushes.White;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

}

