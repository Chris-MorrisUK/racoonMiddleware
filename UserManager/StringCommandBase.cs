using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UserManager
{
    public class StringCommandBase : ICommand
    {
        public StringCommandBase(Action<string> _defaultAction,Predicate<string> _canExecute = null)
        {
            oldCanExecute = false;
            defaultAction = _defaultAction;
            if (_canExecute == null)
                canExecute = DefaultCanExecute;
            else
                canExecute = _canExecute;
            
        }

        protected Predicate<string> canExecute;
        protected Action<string> defaultAction;

        protected bool oldCanExecute;
        #region ICommand Members


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter as string);
        }

        public virtual void Execute(object parameter)
        {
            string strParam = parameter as string;
            if (string.IsNullOrEmpty(strParam)) throw new ArgumentException("Must enter a value");
            defaultAction(strParam);
        }

        private static bool DefaultCanExecute(object parameter)
        {
            string strParam = parameter as string;
            if (strParam == null) return false;
            return !string.IsNullOrEmpty(strParam);
        }
        #endregion
    }
}
