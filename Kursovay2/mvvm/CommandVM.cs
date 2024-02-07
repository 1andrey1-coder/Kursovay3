using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kursovay2.mvvm
{
    public class CommandVM : ICommand
    {

        private readonly Action action;
        private readonly Func<bool> condition;

        public CommandVM(Action action,
            Func<bool> condition)
        {
            this.action = action;
            this.condition = condition;
        }
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return condition();
        }

        public void Execute(object? parameter)
        {
            action();
        }
    }
}
