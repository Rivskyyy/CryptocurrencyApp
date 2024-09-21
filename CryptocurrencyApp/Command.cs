using System.Windows.Input;
using Wpf.Ui.Input;

namespace CryptocurrencyApp
{
    internal class Command : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value;}
            remove { CommandManager.RequerySuggested += value;}
        }

        public Command(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        { 
            return _canExecute == null || _canExecute();
        }

        public async void Execute(object? parameter)
        {
            await _execute();
        }
    }
}
