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

            // Patikriname, ar įvesti visi reikalingi laukai
            if (string.IsNullOrEmpty(Username))
            {
                ErrorMessage += "Username is required.\n";
                
            }
            if (string.IsNullOrEmpty(Password))
            {
                ErrorMessage += "Password field is required.\n";
               
            }

            // Jeigu yra klaidų pranešimų iki šiol, grąžiname false
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return false;
            }

            // Tikriname vartotojo egzistavimą
            if (!_userRepository.UserExists(Username))
            {
                ErrorMessage += "Username doesn't exist.\n";
                return false;
            }

            // Tikriname slaptažodžio teisingumą

            int userId = _userRepository.CheckCredentials(Username, Password);
            if (userId > 0)
            {
                UserSession.InitializeUserSession(userId, Username);
                return true;
            }
            else
            {
                ErrorMessage += "Wrong password.\n";
                return false;
            }

        }

    }
}
