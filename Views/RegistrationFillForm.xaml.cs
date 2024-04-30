using FitTracker.ViewModels;
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

    public partial class RegistrationFillForm : Page
    {
        private RegistrationViewModel registrationViewModel;
        public RegistrationFillForm(RegistrationViewModel viewModel)
        {
            InitializeComponent();
            registrationViewModel = viewModel;
            this.DataContext = registrationViewModel;
            this.KeyDown += Page_KeyDown;
        }

        public event Action OnSubmit;
        private void Submit(object sender, RoutedEventArgs e)
        {

            if (registrationViewModel.IsValidComplete())
            {
                registrationViewModel.CompleteRegister();  // Baigti registraciją ir išsaugoti duomenis DB
                OnSubmit?.Invoke();  // Galbūt naviguoti atgal į prisijungimo puslapį ar pagrindinį puslapį
            }
            else
            {
                MessageBox.Show(registrationViewModel.ErrorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var args = new RoutedEventArgs();

                // Tiesiogiai iškviečiame SignInButt metodą
                Submit(sender, args);
            }
        }
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
