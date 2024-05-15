using FitTracker.Models;
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

namespace FitTracker.Views.UserControls
{

    public partial class ToDoTaskControl : UserControl
    {
        public ToDoTaskControl()
        {
            InitializeComponent();
        }

        public UserToDoListTasks UserToDoListTasks
        {
            get { return (UserToDoListTasks)GetValue(ToDoListTaskProperty); }
            set { SetValue(ToDoListTaskProperty, value); }
        }

        public static readonly DependencyProperty ToDoListTaskProperty =
            DependencyProperty.Register("UserToDoListTasks", typeof(UserToDoListTasks), typeof(ToDoTaskControl), new PropertyMetadata(null));


        public DashboardViewModel ParentViewModel
        {
            get { return (DashboardViewModel)GetValue(ParentViewModelProperty); }
            set { SetValue(ParentViewModelProperty, value); }
        }

        public static readonly DependencyProperty ParentViewModelProperty =
            DependencyProperty.Register("ParentViewModel", typeof(DashboardViewModel), typeof(DailyStatsControl), new PropertyMetadata(null));


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var UserToDoListTasks = this.UserToDoListTasks;
            var parentDataContext = this.ParentViewModel;

            if (UserToDoListTasks != null && parentDataContext != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this task?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    parentDataContext.DeleteToDoListTask(UserToDoListTasks);
                }


            }
        }
    }
}
