using System;
using System.Windows.Input;

namespace GothicRemakeChestHelper.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _can_execute;

        public RelayCommand(Action execute, Func<bool> can_execute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _can_execute = can_execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _can_execute == null || _can_execute();
        public void Execute(object parameter) => _execute();
    }
}