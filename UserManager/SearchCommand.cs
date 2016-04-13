using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UserManager
{
    public class SearchCommand : StringCommandBase
    {
        public SearchCommand(Action<string> _actionIfFound, Action<string> _actionIfNotFound)
            : base(_actionIfFound)
        {
            actionIfNotFound = _actionIfNotFound;
        }

        private Action<string> actionIfNotFound;

        public override void Execute(object parameter)
        {
            string sought = parameter as string;
            if (string.IsNullOrEmpty(sought)) throw new ArgumentException("Must enter a user name");
            if (UserStore.CheckIfUserExists(sought))
                base.defaultAction(sought);
            else
                actionIfNotFound(sought);                
        }


    }
}
