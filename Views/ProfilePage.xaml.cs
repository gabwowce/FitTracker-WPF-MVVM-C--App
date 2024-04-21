using FitTracker.ViewModels;
using FitTracker.Views.UserControls;
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
using static FitTracker.ViewModels.MenuBarViewModel;

namespace FitTracker.Views
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private Frame mainFrame;
        private PageFactory pageFactory;
        private MenuBarViewModel menuBarViewModel;
        

        public ProfilePage(Frame mainFrame, PageFactory pageFactory, MenuBarViewModel menuBarViewModel)
        {
            InitializeComponent();

            this.mainFrame = mainFrame;
            this.pageFactory = pageFactory;
            this.menuBarViewModel = menuBarViewModel;
            this.DataContext = menuBarViewModel;



            AttachEventHandlers();
            
        }

        private void AttachEventHandlers()
        {
            barMenu.OnNavigateToDashboard += () => {
                this.menuBarViewModel.SetActivePage(ActivePage.Dashboard);
                mainFrame.Navigate(pageFactory.CreateDashboardPage());
            };
            barMenu.OnNavigateToDiary += () => {
                this.menuBarViewModel.SetActivePage(ActivePage.Diary);
                mainFrame.Navigate(pageFactory.CreateMyDiaryPage());
            };
            barMenu.OnNavigateToProfile += () => {
                this.menuBarViewModel.SetActivePage(ActivePage.Profile);
                mainFrame.Navigate(this);
            };
        }
    }
}
