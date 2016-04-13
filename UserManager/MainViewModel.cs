using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWareBussinessObjects;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace UserManager
{
    public class MainViewModel: INotifyPropertyChanged
    {
        public MainViewModel()
        {
            if (InDesignMode())//purely for ease of designing the layout - the button visibility changes with this!
                userNew = true;
            else
                userNew = false;
            this.checkForUserCommand = new SearchCommand(
            (name) => UserFound(name),
            (name) => UserNotFound(name)
          
            );
            this.createUserCommand = new StringCommandBase((name) => CreateUser(name));
        }

        
        public static bool InDesignMode()
        {
            return !(Application.Current is App);
        }

        #region commands
        public ICommand CheckForUserCommand
        {
            get { return checkForUserCommand; }
        }
        private SearchCommand checkForUserCommand ;

        public StringCommandBase CreateUserCommand { get { return createUserCommand; } }
        private StringCommandBase createUserCommand;
        #endregion

        #region Methods - called from commands
        public  void UserFound(string name)
        {
            Console.WriteLine("found ");
            UserName = name;
            UserNew = false;
        }

        public   void UserNotFound(string name)
        {
            Console.WriteLine("not found ");
          //  createUserCommand.c
            UserName = name;
            UserNew = true;
        }

        public void CreateUser(string name)
        {
            Console.WriteLine("create luser");
            CurrentUser = new UserBase(name);
            UserNew = false;
        }
        #endregion

        #region properties

        public bool UserIsSet
        {
            get { return CurrentUser != null; }
        }
        UserBase currentUser;

        public UserBase CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (value != currentUser)
                {
                    currentUser = value;
                    OnPropertyChanged("CurrentUser");
                    OnPropertyChanged("UserIsSet");
                }
            }
        }


        bool userNew=false;

        public  bool UserNew
        {
            get
            {
                return userNew;
                
            }
            set
            {
                if (value != userNew)
                {
                    CommandManager.InvalidateRequerySuggested();
                    userNew = value;
                    OnPropertyChanged("UserNew");
                }
            }
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;

            }
            set
            {
                CommandManager.InvalidateRequerySuggested();
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
#endregion

        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }




        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
