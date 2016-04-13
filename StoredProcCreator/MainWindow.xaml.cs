using System;
using System.Collections.Generic;
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
using RacoonMiddleWare;
using System.ComponentModel;
using System.Windows.Threading;
using Queries;

namespace StoredProcCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer statusTextResetTmr;

        public MainWindow()
        {
            InitializeComponent();
            initTimer();
        }

        private void initTimer()
        {
            statusTextResetTmr = new DispatcherTimer();
            statusTextResetTmr.Interval = TimeSpan.FromSeconds(5);
            statusTextResetTmr.Tick += dTimer_Tick;
            statusTextResetTmr.Start();
        }

        void dTimer_Tick(object sender, EventArgs e)
        {
            tbStatusText.Text = "Ready";
        }
        

        #region backing properties for the entered values
        private string qryName;

        public string QryName
        {
            get { return qryName; }
            set 
            {
                mustHaveValue(value);
                mustNotHaveSpaces(value);
                qryName = value;                
            }
        }

        private static void mustNotHaveSpaces(string value)
        {
            if (value.Contains(" "))
                throw new ArgumentOutOfRangeException("Name must not contain spaces");
        }
        private string queryType;

        public string QueryType
        {
            get { return queryType; }
            set { queryType = value; }
        }
        private string contentText;

        public string ContentText
        {
            get { return contentText; }
            set 
            {
                mustHaveValue(value);
                contentText = value; 
            }
        }

        private string server;

        public string Server
        {
            get { return server; }
            set
            {
                mustNotHaveSpaces(value);
                server = value; 
            }

        }


        private string dataStore;

        public string DataStore
        {
            get { return dataStore; }
            set
            {
                mustNotHaveSpaces(value);
                dataStore = value;
            }
        }
        #endregion
        private static void mustHaveValue(string toCheck)
        {
            if (string.IsNullOrEmpty(toCheck))
                throw new ArgumentOutOfRangeException("Content must not be empty");
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StoredProcList.GetSPList().CreateAndAdd(QryName, QueryType, ContentText,Server,DataStore);
            tbStatusText.Text = "Added item";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(StoredProcList.GetSPList().IsEmpty)
                if(MessageBox.Show("Do you want to save an empty list?","Nothing added to list",MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            //TODO: make the default be the right place
            Microsoft.Win32.SaveFileDialog svDlg = new Microsoft.Win32.SaveFileDialog();
            svDlg.Filter = @"Racoon Settings|*.rst|All Files|*.*";
            svDlg.DefaultExt = "*.rst";
            svDlg.Title = "Select target for settings file";
            if (svDlg.ShowDialog().Value)
            {
                StoredProcList.GetSPList().SaveToFile(svDlg.FileName);
                tbStatusText.Text = "Saved";
            }
            else
                tbStatusText.Text = "Save Canceled";

        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            tbStatusText.Text = Queries.RedisQuery.GetAssembly();
            Console.WriteLine(tbStatusText.Text);
        }
        
    }
}
