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
    /// <summary>
    /// Interaction logic for MyCustomToggleButton.xaml
    /// </summary>
    public partial class MyCustomToggleButton : UserControl
    {
        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register(
        "ButtonContent", typeof(string), typeof(MyCustomToggleButton), new PropertyMetadata(default(string), OnButtonContentChanged));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        "Command", typeof(ICommand), typeof(MyCustomToggleButton), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public MyCustomToggleButton()
        {
            InitializeComponent();
        }

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        private static void OnButtonContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MyCustomToggleButton)d;
            control.toggleButton.Content = e.NewValue;
        }
    }
}
