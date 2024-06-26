﻿using FitTracker.ViewModels;
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

            var viewModel = new MyDiaryPageViewModel(menuBarViewModel);
            this.DataContext = viewModel;

            AttachEventHandlers();
            this.Loaded += async (sender, e) => await viewModel.InitializeAsync();


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

        private void CalendarButt(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dayInfo = button.CommandParameter as DayInfo; // 'DayInfo' yra klasė, kurią naudojate savo WeekDays sąraše.

            if (dayInfo != null)
            {
                // Naviguoja į AddDayPage su paspaustos dienos data
                var addDayPage = pageFactory.CreateAddDayPage(dayInfo.MonthDay);
                mainFrame.Navigate(addDayPage);
                NavigateBackToMain(addDayPage);
            }
        }

        private void OpenAddDayPageWithCurrentDate(object sender, RoutedEventArgs e)
        {
            var todayDate = DateTime.Now.ToString("MMMM dd");
            var addDayPage = pageFactory.CreateAddDayPage(todayDate);
            mainFrame.Navigate(addDayPage);
            NavigateBackToMain(addDayPage);
        }

        private void NavigateBackToMain(AddDayPage addDayPage)
        {
            addDayPage.onCancelAddDay += () => mainFrame.Navigate(this);
            addDayPage.onSaveAddDay += () => mainFrame.Navigate(this);
        }

        public event Action onAddDayClick;

        private void AddDayButt(object sender, RoutedEventArgs e)
        {
            onAddDayClick?.Invoke();
        }


    }
}
