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

    public partial class MenuBar : UserControl
    {

        public event Action OnNavigateToDashboard;
        public event Action OnNavigateToDiary;
        public event Action OnNavigateToProfile;


        public MenuBar()
        {
            InitializeComponent();

        }

        private void MyProfileNavButt(object sender, RoutedEventArgs e)
        {
            OnNavigateToProfile?.Invoke();
        }

        private void MyDiaryNavButt(object sender, RoutedEventArgs e)
        {
            OnNavigateToDiary?.Invoke();
        }

        private void DashboardNavButt(object sender, RoutedEventArgs e)
        {
            OnNavigateToDashboard?.Invoke();
        }
    }
}
