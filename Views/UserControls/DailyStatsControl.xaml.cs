using FitTracker.Models;
using FitTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FitTracker.Views.UserControls
{

    public partial class DailyStatsControl : UserControl
    {
        public DailyStatsControl()
        {
            InitializeComponent();
        }

        public DayRecord DayRecord
        {
            get { return (DayRecord)GetValue(DayRecordProperty); }
            set { SetValue(DayRecordProperty, value); }
        }

        public static readonly DependencyProperty DayRecordProperty =
            DependencyProperty.Register("DayRecord", typeof(DayRecord), typeof(DailyStatsControl), new PropertyMetadata(null));

        public MyDiaryPageViewModel ParentViewModel
        {
            get { return (MyDiaryPageViewModel)GetValue(ParentViewModelProperty); }
            set { SetValue(ParentViewModelProperty, value); }
        }

        public static readonly DependencyProperty ParentViewModelProperty =
            DependencyProperty.Register("ParentViewModel", typeof(MyDiaryPageViewModel), typeof(DailyStatsControl), new PropertyMetadata(null));

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var dayRecord = this.DayRecord;
            var parentDataContext = this.ParentViewModel;

            if(dayRecord != null && parentDataContext != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    parentDataContext.DeleteDayRecord(dayRecord);
                }

               
            }
        }


    }

}
