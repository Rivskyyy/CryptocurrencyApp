using System.Windows.Input;
using Wpf.Ui.Input;

namespace CryptocurrencyApp
{
    internal class Command : ICommand
    {
        private readonly Func<Task> _executeAsync;
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value;}
            remove { CommandManager.RequerySuggested += value;}
        }
        public Command(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public Command(Func<Task> execute, Func<bool> canExecute = null)
        {
            _executeAsync = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        { 
            return _canExecute == null || _canExecute();
        }

        public void Execute(object? parameter) 
        {
            if (_executeAsync != null)
            {
                ExecuteAsync(parameter);
            }
            else
            {
                _execute?.Invoke();
            }
        
        }

        public async void ExecuteAsync(object? parameter)
        {
            await _executeAsync();
        }

        

    }
}
