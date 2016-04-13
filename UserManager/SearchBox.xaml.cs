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
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl, INotifyPropertyChanged
    {
        public SearchBox()
        {
            InitializeComponent();

        }

        public ICommand ExternalSearchCommand
        {
            get
            {
                return (ICommand)GetValue(ExternalSearchCommandProperty);
            }
            set
            {
                SetValue(ExternalSearchCommandProperty, value);
            }
        }

        public static readonly DependencyProperty ExternalSearchCommandProperty =
            DependencyProperty.Register("ExternalSearchCommand", typeof(ICommand), typeof(SearchBox), new UIPropertyMetadata(null));

        public string SearchString
        {
            get
            {
                string result = GetValue(SearchStringProperty) as string;
                if (result != null)
                    return result;
                else return string.Empty;
            }
            set
            {
                CommandManager.InvalidateRequerySuggested();
                SetValue(SearchStringProperty, value);
                OnPropertyChanged("SearchString");

            }
        }

        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        public static readonly DependencyProperty SearchStringProperty =
    DependencyProperty.Register("SearchString", typeof(string), typeof(SearchBox), new UIPropertyMetadata(null));




        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ExternalSearchCommand != null)
                ExternalSearchCommand.Execute(txtSearch.Text);
        }



    }
}
