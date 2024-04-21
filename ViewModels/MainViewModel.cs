using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Event used to propagate changes to the UI
        public event PropertyChangedEventHandler PropertyChanged;

        // Action delegate for performing navigation
        public Action<string> NavigateAction { get; set; }

        // Example of how you might notify property changes
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Example method that could trigger navigation
        public void PerformNavigation(string destination)
        {
            NavigateAction?.Invoke(destination);
        }
    }
}
