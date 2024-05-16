using FitTracker.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace FitTracker.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _originalUsername;
        private string _gender;
        private int _height;
        private float _weight;
        private DateTime _dateOfBirth;
        private readonly UserRepository _repository;
        public MenuBarViewModel MenuBar { get; private set; }
        public int UserId { get; set; }

        public ProfileViewModel(MenuBarViewModel menuBarViewModel, int userId)
        {
            _repository = new UserRepository(ConfigurationManager.ConnectionStrings["MyConnectionToDB"].ConnectionString);
            MenuBar = menuBarViewModel;
            LoadUserProfile(userId);
        }

        private async void LoadUserProfile(int userId)
        {
            var userProfile = await _repository.GetUserProfileAsync(userId);
            if (userProfile != null)
            {
                UserId = userProfile.UserId;
                Username = userProfile.Username;
                _originalUsername = userProfile.Username;
                Gender = userProfile.Gender;
                Weight = userProfile.Weight;
                Height = userProfile.Height;
                DateOfBirth = userProfile.DateOfBirth;
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        public float Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public bool SaveProfile()
        {
            if (_repository.UserExists(Username) && Username != _originalUsername)
            {

                Username = _originalUsername;
                return false; 
            }

            var userProfile = new User
            {
                UserId = UserSession.UserId,
                Username = this.Username,
                Gender = this.Gender,
                Weight = this.Weight,
                Height = this.Height,
                DateOfBirth = this.DateOfBirth
            };

            _repository.UpdateUser(userProfile);
            _originalUsername = this.Username; 
            return true; 

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
