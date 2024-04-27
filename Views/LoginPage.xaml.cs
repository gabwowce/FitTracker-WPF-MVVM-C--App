using FitTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginViewModel loginViewModel;
        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();

            loginViewModel = viewModel;
            this.DataContext = loginViewModel;
        }

        public event Action OnLoginSuccess;
        public event Action OnNavigateToRegistration;

        private void SignInButt(object sender, RoutedEventArgs e)
        {

            var password = PasswordBox.Password;

            loginViewModel.Password = password;
            

            if (loginViewModel.IsValidSignIn())
            {
                OnLoginSuccess?.Invoke();
            }
            else
            {
                MessageBox.Show(loginViewModel.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterNavButt(object sender, RoutedEventArgs e)
        {
            OnNavigateToRegistration?.Invoke();
        }

    }
}

