using FitTracker.Models;
using FitTracker.ViewModels;
using FitTracker.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitTracker.Views
{
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; private set; }
        public MenuBar menuBar { get; private set; }

        private PageFactory pageFactory;

        private MenuBarViewModel menuBarViewModel;

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
            menuBarViewModel = new MenuBarViewModel();

            pageFactory = new PageFactory(MainFrame, menuBarViewModel);

            // Sukurti puslapius per PageFactory
            var loginPage = pageFactory.CreateLoginPage();
            var registrationPage = pageFactory.CreateRegistrationPage(); // Asumuojame, kad ši funkcija yra sukurta
            var registrationFillForm = pageFactory.CreateRegistrationFillForm(); // Asumuojame, kad ši funkcija yra sukurta

            // Prisegti įvykių tvarkytojus
            InitializeEventHandlers(loginPage, registrationPage, registrationFillForm);

            // Parodyti pradinį puslapį
            MainFrame.Navigate(loginPage);
        }

        private void InitializeEventHandlers(LoginPage loginPage, RegistrationPage registrationPage, RegistrationFillForm registrationFillForm)
        {
            // LoginPage įvykių tvarkytojai
            loginPage.OnLoginSuccess += () => MainFrame.Navigate(pageFactory.CreateMyDiaryPage());
            loginPage.OnNavigateToRegistration += () => MainFrame.Navigate(registrationPage);

            // RegistrationPage įvykių tvarkytojai
            registrationPage.OnRegistrationSuccess += () => MainFrame.Navigate(registrationFillForm);
            registrationPage.OnNavigateToLogin += () => MainFrame.Navigate(loginPage);

            // RegistrationFillForm įvykių tvarkytojas
            registrationFillForm.OnSubmit += () => MainFrame.Navigate(pageFactory.CreateMyDiaryPage());
        }
    }
}
