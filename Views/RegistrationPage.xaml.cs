﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitTracker.Views
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
           
        }


        public event Action OnRegistrationSuccess;
        public event Action OnNavigateToLogin;

        private void SignInButt(object sender, RoutedEventArgs e)
        {
            OnNavigateToLogin?.Invoke();
        }

        private void CompleteRegistration(object sender, RoutedEventArgs e)
        {
            OnRegistrationSuccess?.Invoke();
        }
    }
}
