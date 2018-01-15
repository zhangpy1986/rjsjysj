using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Contacts
{
    public class RelayCommand : RoutedUICommand
    {
        public RelayCommand(Action<object> exeCallback, Func<object, bool> canExeCallback)
        {
            this.canExecute = canExeCallback;
            this.execute = exeCallback;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            execute?.Invoke(parameter);
        }

        private Action<object> execute;
        private Func<object, bool> canExecute;
    }
}
