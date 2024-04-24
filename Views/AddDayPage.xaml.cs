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
    public partial class AddDayPage : Page
    {
        private DayInfo dayInfo;
        private string date;
        private MenuBarViewModel menuBarViewModel;
        public AddDayPage(string date)
        {
            this.DataContext = new MyDiaryPageViewModel(menuBarViewModel);
            InitializeComponent();
            this.date = date;
        }
    }
}
