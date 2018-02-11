using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MultiDownloader.Commands
{
    class UserCommands : ICommand
    {
        private Action whatToExecute;
        private Func<bool> whenToExecute;

        public UserCommands(Action _whatToExecute, Func<bool> _whenToExecute)
        {
            this.whatToExecute = _whatToExecute;
            this.whenToExecute = _whenToExecute;
        }
        public bool CanExecute(object parameter)
        {
            return this.whenToExecute();
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            this.whatToExecute();
        }

       
    }
}
