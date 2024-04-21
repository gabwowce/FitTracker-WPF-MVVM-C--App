using System;
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
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public event Action OnLoginSuccess;
        public event Action OnNavigateToRegistration;

        private void SignInButt(object sender, RoutedEventArgs e)
        {
            // Tikrinkite prisijungimo kredencialus ir, jei sėkminga, iškvieskite įvykį
            OnLoginSuccess?.Invoke();
        }

        private void RegisterNavButt(object sender, RoutedEventArgs e)
        {
            OnNavigateToRegistration?.Invoke();
        }

    }
}

