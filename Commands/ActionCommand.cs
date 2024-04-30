using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitTracker.Commands
{
    internal class ActionCommand : ICommand
    {
        private readonly Action<string> _action;

        public ActionCommand(Action<string> action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true; // arba įgyvendinti logiką, kada komanda gali būti vykdoma
        }

        public void Execute(object parameter)
        {
            _action?.Invoke(parameter as string);
        }
    }
}
