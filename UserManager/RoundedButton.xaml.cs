using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserManager
{
    /// <summary>
    /// Interaction logic for RoundedButton.xaml
    /// </summary>
    public partial class RoundedButton : UserControl//, INotifyPropertyChanged
    {
        public RoundedButton()
        {
            InitializeComponent();
            visible = true;
            DataContext = this;
            Binding fuckxaml = new Binding();
            fuckxaml.Source = OnClickCmd;
            btnCreateUser.SetBinding(Button.CommandProperty, fuckxaml);
        }



        private string text;

        public string Text
        {
            get { return text; }
            set 
            { 
                text = value;
            //    OnPropertyChanged("Text");
                SetValue(TextProperty, value);
            }
        }

        private ICommand onClickCmd;

        public ICommand OnClickCmd
        {
            get { return onClickCmd; }
            set
            {
                onClickCmd = value;
                //OnPropertyChanged("OnClickCmd");
                SetValue(OnClickCmdProperty, value);
            }
        }

        private object commandParam;

        public object CommandParam
        {
            get { return commandParam; }
            set
            {
                commandParam = value;
                SetValue(CommandPramProperty, value);
               // OnPropertyChanged("CommandParam");
            }
        }

        private bool visible;

        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                SetValue(VisProperty, value);
                //OnPropertyChanged("Visible");
            }
        }



        //private void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
               
              
        //}



        #region INotifyPropertyChanged Members

        //public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region dependency properties
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
    "Text",
    typeof(string),
    typeof(RoundedButton),
    new FrameworkPropertyMetadata
    {
        DefaultValue = string.Empty,
        DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
    });

        public static readonly DependencyProperty OnClickCmdProperty = DependencyProperty.RegisterAttached(
    "OnClickCmd",
    typeof(ICommand),
    typeof(RoundedButton),
    new FrameworkPropertyMetadata());

        public static readonly DependencyProperty CommandPramProperty = DependencyProperty.RegisterAttached(
    "CommandParam",
    typeof(Object),
    typeof(RoundedButton),
    new FrameworkPropertyMetadata());

        public static readonly DependencyProperty VisProperty = DependencyProperty.RegisterAttached(
    "Visible",
    typeof(bool),
    typeof(RoundedButton),
    new FrameworkPropertyMetadata
    {
        DefaultValue = true,
        DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
    });
        #endregion
    }
}
