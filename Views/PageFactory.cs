using FitTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            this.mainFrame = mainFrame;
            this.menuBarViewModel = menuBarViewModel;
        }

        public RegistrationPage CreateRegistrationPage()
        {
            return registrationPage ?? (registrationPage = new RegistrationPage());

        }

        public RegistrationFillForm CreateRegistrationFillForm()
        {
            return registrationFillForm ?? (registrationFillForm = new RegistrationFillForm());
        }

        public LoginPage CreateLoginPage()
        {
            return loginPage ?? (loginPage = new LoginPage());
        }

        public AddDayPage CreateAddDayPage()
        {
            return addDayPage ?? (addDayPage = new AddDayPage());
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
