using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiddleWareBussinessObjects;

namespace WinFormsUserManager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            pnlUserDetails.Visible = false;
            initTimer();
        }

        private void initTimer()
        {
            
            tmrDisplay = new Timer();
            tmrDisplay.Interval = 5000;
            tmrDisplay.Tick += tmrDisplay_Tick;            
        }

        Timer tmrDisplay;
        UserBase CurrentUser;
        Uri userUri;
        bool createNotEdit;

        public bool CreateNotEdit
        {
            get { return createNotEdit; }
            set 
            {
                //if (value != createNotEdit)
                //{
                    createNotEdit = value;
                    if (createNotEdit)
                        btnCreate.Text = "Create";
                    else
                        btnCreate.Text = "Edit";

               // }
            }
        }

        void tmrDisplay_Tick(object sender, EventArgs e)
        {
            tmrDisplay.Stop();
            lbStatus.Text = "Ready";
        }

        private void displayStatus(string message)
        {
            lbStatus.Text = message;
            tmrDisplay.Start();
        }
   

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (UserStore.CheckIfUserExists(txtSearch.Text))
            {
                editUser();
            }
            else
            {
                createUser();
            }
        }

        private void editUser()
        {
            CreateNotEdit = false;
            CurrentUser = UserStore.GetUser(txtSearch.Text);
            updateControls(CurrentUser);
            pnlUserDetails.Visible = true;
        }

        private void updateControls(UserBase selUser)
        {
            chkValid.Checked = selUser.IsVald;
            txtUri.Text = selUser.UniversalID != null ? selUser.UniversalID.ToString() : string.Empty;
            txtPassword.Text = "<not shown>";

        }            

        private void createUser()
        {
            CreateNotEdit = true;
            displayStatus("User does not exist, create user");
            CurrentUser = new UserBase(txtSearch.Text);
            pnlUserDetails.Visible = true;

        }

        private void txtSearch_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                userNameValid = false;
                return;
            }
            if(txtSearch.Text.Contains(" "))
            {
                userNameValid = false;
                return;
            }
            userNameValid = true;
        }
        bool userNameValid=false;

        public bool UserNameValid
        {
            get { return userNameValid; }
            set
            {
                userNameValid = value;
                btnSearch.Enabled = userNameValid;
                if (!userNameValid)
                {
                    displayStatus("Username is not valid");
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            updateUserFromControls();
            UserStore.AddUser(CurrentUser);//if we're in edit mode this will just smash the new user over the top of the old one
            displayStatus("User Created");
        }

        private void updateUserFromControls()
        {
            CurrentUser.SetPassword(txtPassword.Text);
            if (userUri != null)
                CurrentUser.UniversalID = userUri;
            CurrentUser.IsVald = chkValid.Checked;
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {            
            TextBox tbToValidate = sender as TextBox;
            if (string.IsNullOrEmpty(tbToValidate.Text))
                e.Cancel = true;
            else if (txtPassword.Text.Length < 8)
                e.Cancel = true;
         //   btnSetPassword.Enabled = !e.Cancel;
            if (e.Cancel)
                markAsFailed(tbToValidate);
            else
                markAsNormal(tbToValidate);
        }
        
        private void txtUri_Validating(object sender, CancelEventArgs e)
        {
            TextBox tbToValidate = sender as TextBox;
            if (string.IsNullOrEmpty(tbToValidate.Text))
                e.Cancel = true;
            e.Cancel = !Uri.TryCreate(tbToValidate.Text, UriKind.RelativeOrAbsolute, out userUri);
            if (e.Cancel)
                markAsFailed(tbToValidate);
            else
                markAsNormal(tbToValidate);
        }

        private static void markAsFailed(TextBox failed)
        {
            failed.BackColor = Color.Red;
            failed.ForeColor = Color.Yellow;            
        }

        private static void markAsNormal(TextBox normal)
        {
            normal.BackColor = SystemColors.Window;
            normal.ForeColor = SystemColors.WindowText;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        //private void btnSetPassword_Click(object sender, EventArgs e)
        //{
        //    CurrentUser.SetPassword(txtPassword.Text);
        //    displayStatus("Password set");
        //    txtPassword.Text = string.Empty;
        //}

    }
}

