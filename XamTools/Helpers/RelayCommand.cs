using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eoTouchx.Helpers
{
    [DebuggerStepThrough]
    public class RelayCommand : ICommand
    {
        readonly Func<object, bool> _canExecute;
        readonly Func<object, Task> _action;
        public RelayCommand(Func<object, Task> action)
        {
            _action = action;
        }
        public RelayCommand(Func<object, Task> action, Func<object, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }
        public RelayCommand(Action<object> action)
        {
            _action = parameter => {
                action(parameter);
                return Task.FromResult(true);
            };
        }
        public RelayCommand(Action<object> action, Func<object, bool> canExecute)
        {
            _action = parameter => {
                action(parameter);
                return Task.FromResult(true);
            };
            _canExecute = canExecute;
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool Executing { get; private set; }
        public Action FinishedCallback { get; set; } = null;
        public int Timeout { get; set; }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {

            if (!CanExecute(parameter))
                return;

            Executing = true;

            _action(parameter).ContinueWith(task =>
            {
                Executing = false;
                FinishedCallback?.Invoke();
            });
        }
    }

    /// <summary>
    /// Generic RelayCommand class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerStepThrough]
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// The execute
        /// </summary>
        readonly Action<T> _execute;

        /// <summary>
        /// The can execute
        /// </summary>
        readonly Predicate<T> _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute predicate.</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));

            if (canExecute != null)
                _canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raise <see cref="RelayCommand{T}.CanExecuteChanged" /> event.
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns><c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) != true;

        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public virtual void Execute(object parameter)
        {
            if (CanExecute(parameter))
                _execute((T)parameter);
        }
    }
}