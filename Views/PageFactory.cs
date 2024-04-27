using FitTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FitTracker.Views
{
    public class PageFactory
    {
        private Frame mainFrame;
        private MenuBarViewModel menuBarViewModel;
        private LoginPage loginPage;
        private DashboardPage dashboardPage;
        private ProfilePage profilePage;
        private MyDiaryPage myDiaryPage;
        private RegistrationPage registrationPage;
        private RegistrationFillForm registrationFillForm;
        private AddDayPage addDayPage;



        public PageFactory(Frame mainFrame, MenuBarViewModel menuBarViewModel)
        {
            /*RegistrationViewModel registrationViewModel = new RegistrationViewModel();*/
            /*LoginViewModel loginViewModel = new LoginViewModel();*/
            this.mainFrame = mainFrame;
            this.menuBarViewModel = menuBarViewModel;
          /*  this.loginPage = CreateLoginPage(loginViewModel);*/

        }

        public RegistrationPage CreateRegistrationPage(RegistrationViewModel registrationViewModel)
        {
            return registrationPage ?? (registrationPage = new RegistrationPage(registrationViewModel));

        }

        public RegistrationFillForm CreateRegistrationFillForm(RegistrationViewModel registrationViewModel)
        {
            return registrationFillForm ?? (registrationFillForm = new RegistrationFillForm(registrationViewModel));
        }

        public LoginPage CreateLoginPage(LoginViewModel loginViewModel)
        {
            return loginPage ?? (loginPage = new LoginPage(loginViewModel));
        }

        public AddDayPage CreateAddDayPage(string date)
        {
            return new AddDayPage(date);
        }

        public DashboardPage CreateDashboardPage()
        {
            return dashboardPage ?? (dashboardPage = new DashboardPage(mainFrame, this, menuBarViewModel));

        }

        public ProfilePage CreateProfilePage()
        {
            return profilePage ?? (profilePage = new ProfilePage(mainFrame, this, menuBarViewModel));
        }

        public MyDiaryPage CreateMyDiaryPage()
        {
            return myDiaryPage ?? (myDiaryPage = new MyDiaryPage(mainFrame, this, menuBarViewModel));
        }
    }
}
