using System;
using System.Windows.Input;

namespace WPF_Productos.Core
{
    public class RelayCommand : ICommand
    {
        private Action _action;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public RelayCommand(Action action)
        {
            _action = action;
            _canExecute = () => true;
        }

        public bool CanExecute(object parameter)
        {
            bool result = _canExecute.Invoke();
            return result;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
