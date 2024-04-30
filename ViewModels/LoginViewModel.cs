using FitTracker.Models;
using FitTracker.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;


namespace FitTracker.ViewModels
{
    public class LoginViewModel 

    {

        private LoginPage loginPage;
        private UserRepository _userRepository;
        public string Username { get; set; }
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LoginViewModel()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionToDB"].ConnectionString;
            _userRepository = new UserRepository(connectionString);
        }


        public bool IsValidSignIn()
        {
            ErrorMessage = "";
            if (string.IsNullOrEmpty(Username)) ErrorMessage += "Username is required.\n";
            if (string.IsNullOrEmpty(Password)) ErrorMessage += "Password field is required.\n";
            if (!_userRepository.UserExists(Username)) ErrorMessage += "Username doesn't exists.\n";
            if (_userRepository.CorrectPassword(Username, Password)) ErrorMessage += "Wrong password.\n";
            

            return string.IsNullOrEmpty(ErrorMessage);
        }

    }
}
