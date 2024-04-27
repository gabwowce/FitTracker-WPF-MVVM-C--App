﻿using FitTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class AddDayPage : Page
    {
        private DayInfo dayInfo;
        public string Date { get; private set; }

        public AddDayPage(string date)
        {

            InitializeComponent();
            this.Date = date;

            this.DataContext = new AddDayViewModel(ConfigurationManager.ConnectionStrings["MyConnectionToDB"].ConnectionString);

        }


        public event Action onCancelAddDay;
        public event Action onSaveAddDay;

        private void CancelButt(object sender, RoutedEventArgs e)
        {
            onCancelAddDay?.Invoke();
        }

        private void SaveButt(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as AddDayViewModel;
            if (viewModel != null)
            {
                viewModel.SaveRecord();
            }

            onSaveAddDay?.Invoke();
        }
    }
    
}
