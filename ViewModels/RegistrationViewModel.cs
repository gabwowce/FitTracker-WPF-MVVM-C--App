using FitTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Configuration;
using Microsoft.Win32;
using FitTracker.Views;

namespace FitTracker.ViewModels
{
    public class RegistrationViewModel
    {
        private UserRepository _userRepository;

        private RegistrationPage registrationPage;

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public float? Weight { get; set; } // naudojamas nullable float, kuris gali būti null pradžioje
        public int? Height { get; set; } // naudojamas nullable int, kuris gali būti null pradžioje

        public DateTime DateOfBirth { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRegistrationSuccessful { get; set; }

        public ICommand RegisterCommand { get; private set; }
        public ICommand CompleteRegistrationCommand { get; private set; }

        public RegistrationViewModel()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionToDB"].ConnectionString;
            _userRepository = new UserRepository(connectionString);

           /* RegisterCommand = new RelayCommand(PerformInitialRegistration, IsValidInitial);
            CompleteRegistrationCommand = new RelayCommand(CompleteRegister, IsValidComplete);*/
        }


        public bool IsValidInitial()
        {
            ErrorMessage = "";
            if (string.IsNullOrEmpty(Username)) ErrorMessage += "Username is required.\n";
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword)) ErrorMessage += "Both password fields are required.\n";
            if (Password != ConfirmPassword) ErrorMessage += "Passwords do not match.\n";
            if (_userRepository.UserExists(Username)) ErrorMessage += "Username already exists.\n";

            return string.IsNullOrEmpty(ErrorMessage);
        }

        public bool IsValidSignIn()
        {
            ErrorMessage = "";
            if (string.IsNullOrEmpty(Username)) ErrorMessage += "Username is required.\n";
            if (string.IsNullOrEmpty(Password)) ErrorMessage += "Password field is required.\n";
            if (_userRepository.CorrectPassword(Username, Password)) ErrorMessage += "Wrong password.\n";
            if (_userRepository.UserExists(Username)) ErrorMessage += "Username doesn't exists.\n";

            return string.IsNullOrEmpty(ErrorMessage);
        }

        private void PerformInitialRegistration()
        {
            if (IsValidInitial())
            {
                IsRegistrationSuccessful = true;


            }
            else
            {
                IsRegistrationSuccessful = false;
            }
        }

        public void CompleteRegister()
        {
            if (!IsValidComplete()) return;

            string passwordHash = HashHelper.HashPassword(Password);
            User newUser = new User
            {
                Username = Username,
                PasswordHash = passwordHash,
                Gender = Gender,
                Height = Height.HasValue ? Height.Value : 0, // 0 arba kita numatytoji reikšmė
                Weight = Weight.HasValue ? Weight.Value : 0, // 0 arba kita numatytoji reikšmė
                DateOfBirth = DateOfBirth,
                RegistrationDate = DateTime.Now
            };

            _userRepository.AddUser(newUser);
        }

        
        public bool IsValidComplete()
        {
            ErrorMessage = "";
            if (string.IsNullOrEmpty(Gender) || Gender == "Select Gender") ErrorMessage += "Gender is required.\n";
            if (Height <= 0) ErrorMessage += "Height must be greater than zero.\n";
            if (Weight <= 0) ErrorMessage += "Weight must be greater than zero.\n";
            if (DateOfBirth >= DateTime.Now) ErrorMessage += "Date of Birth must be in the past.\n";

            return string.IsNullOrEmpty(ErrorMessage);
        }
    }
}


