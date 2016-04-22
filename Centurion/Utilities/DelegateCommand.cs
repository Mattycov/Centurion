using System;
using System.Windows.Input;

namespace Centurion.Core.Utilities
{
  public class DelegateCommand : ICommand
  {
    private readonly Predicate<object> canExecute;
    private readonly Action execute;

    public event EventHandler CanExecuteChanged;

    public DelegateCommand(Action execute)
                   : this(execute, null)
    {
    }

    public DelegateCommand(Action execute,
                   Predicate<object> canExecute)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return canExecute == null || canExecute(parameter);
    }

    public void Execute(object parameter)
    {
      execute();
    }

    public void RaiseCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}
