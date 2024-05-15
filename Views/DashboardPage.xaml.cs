using FitTracker.Models;
using FitTracker.ViewModels;
using FitTracker.Views.UserControls;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace FitTracker.Views
{
    public partial class DashboardPage : Page
    {
        public ObservableCollection<string> TaskCollection { get; set; }

        private Frame mainFrame;
        private PageFactory pageFactory;
        private MenuBarViewModel menuBarViewModel;

        public DashboardPage(Frame mainFrame, PageFactory pageFactory, MenuBarViewModel menuBarViewModel)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            this.pageFactory = pageFactory;
            this.menuBarViewModel = menuBarViewModel;
            int userId = UserSession.UserId;

            var viewModel = new DashboardViewModel(menuBarViewModel,userId);
            this.DataContext = viewModel;

            AttachEventHandlers();
            this.Loaded += async (sender, e) => await viewModel.InitializeAsync();
        }

        private void AttachEventHandlers()
        {
            barMenu.OnNavigateToDashboard += () => {
                this.menuBarViewModel.SetActivePage(MenuBarViewModel.ActivePage.Dashboard);
                mainFrame.Navigate(this);
            };
            barMenu.OnNavigateToDiary += () => {
                this.menuBarViewModel.SetActivePage(MenuBarViewModel.ActivePage.Diary);
                mainFrame.Navigate(pageFactory.CreateMyDiaryPage());
            };
            barMenu.OnNavigateToProfile += () => {
                this.menuBarViewModel.SetActivePage(MenuBarViewModel.ActivePage.Profile);
                mainFrame.Navigate(pageFactory.CreateProfilePage());
            };
        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var viewModel = this.DataContext as DashboardViewModel;
            if (viewModel != null)
            {
                System.Diagnostics.Debug.Write("---------->Saved<-------------");
                viewModel.SaveRecord();
            }
        }
    }
}
