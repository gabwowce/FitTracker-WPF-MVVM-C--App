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


    public partial class MyDiaryPage : Page
    {

        private Frame mainFrame;
        private PageFactory pageFactory;
        private MenuBarViewModel menuBarViewModel;


        public MyDiaryPage(Frame mainFrame, PageFactory pageFactory, MenuBarViewModel menuBarViewModel)
        {
            InitializeComponent();

            this.mainFrame = mainFrame;
            this.pageFactory = pageFactory;
            this.menuBarViewModel = menuBarViewModel;

            menuBarViewModel.SetActivePage(ActivePage.Diary);

            this.DataContext = new MyDiaryPageViewModel(menuBarViewModel);


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
                mainFrame.Navigate(this);
            };
            barMenu.OnNavigateToProfile += () => {
                this.menuBarViewModel.SetActivePage(ActivePage.Profile);
                mainFrame.Navigate(pageFactory.CreateProfilePage());
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Heeloo");
        }
    }
}
