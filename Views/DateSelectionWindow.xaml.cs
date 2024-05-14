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
using System.Windows.Shapes;

namespace FitTracker.Views
{
    
    public partial class DateSelectionWindow : Window
    {
        public DateTime SelectedDate { get; private set; }
        public DateSelectionWindow()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate.HasValue)
            {
                SelectedDate = datePicker.SelectedDate.Value;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please select a date.");
            }
        }
    }
}
