using Driving___Vehicle_License_Department__DVLD_.Properties;
using Driving___Vehicle_License_Department__DVLD_.UserControls;
using DVLD.Classes;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Driving___Vehicle_License_Department__DVLD_
{
    public partial class frmAddNewUser : Form
    {

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;
        
        private int _UserID = -1;
        
        clsUser _User;
        
        //private clsPerson _Person;
        
        public frmAddNewUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        
        
        public frmAddNewUser(int UserID)
       {
            InitializeComponent();
            _UserID = UserID;
            _Mode   = enMode.Update;

        }


        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();
                tpLoginInfo.Enabled = false;

            }
            else
            {

                lblMode.Text = "Update User";
                this.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            cbIsActive.Checked = true;


        }


        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();


        }
        
        
        
        private void _LoadData()
        {

            _User   = clsUser.FindByUserID(_UserID);
            ucFilterPerson1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show($"this form will be closed because no User with ID Found {_UserID}");
                this.Close();
                return;
            }

            ucFilterPerson1.LoadPersonInfo(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();

            txtUserName.Text        = _User.UserName;
            txtPassword.Text        = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            cbIsActive.Checked      = _User.IsActive;

            if (clsGlobal.CurrentUser.UserID != _UserID)
            {
                txtUserName.Enabled = false;
            }
            else
            {
                cbIsActive.Enabled = false;
            }

            btnShowHidePassword.Enabled = false;
            txtPassword.Enabled = false;
            txtConfirmPassword.Enabled = false;
           

        }
        
        private void btnBack_Click(object sender, EventArgs e)
        {
            
            tcUserInfo.Selecting -= tabControl1_Selecting;  // Temporarily unblock
            tcUserInfo.SelectedTab = tpPersonalInfo;        // or any other tab
            tcUserInfo.Selecting += tabControl1_Selecting;  // Reblock
        }
        
        private void btnNext_Click(object sender, EventArgs e)
        {
       
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                
                GoToTabLoginInfo();
                return;
            }

            

            if (ucFilterPerson1.PersonID != -1)
            {

                if (clsUser.IsUserExistsForPersonID(ucFilterPerson1.PersonID))
                {
                    MessageBox.Show("Selected person already has used Choose another one", "Select another person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;

                    GoToTabLoginInfo();

                }

            }
            else
            {
                MessageBox.Show($"Please Select a Person.", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

        }
        
        
        
        
        private void GoToTabLoginInfo()
        {
            tcUserInfo.Selecting -= tabControl1_Selecting;  // Temporarily unblock
            tcUserInfo.SelectedTab = tpLoginInfo;           // or any other tab
            tcUserInfo.Selecting += tabControl1_Selecting;  // Reblock

        }
        
        
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon", "Validation Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            
            _User.PersonID = ucFilterPerson1.PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = clsUtil.ComputeHash(txtPassword.Text);
            _User.IsActive = cbIsActive.Checked;

            if (_User.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error : data is not Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Mode          = enMode.Update;
            this.Text      = "Update User";
            lblMode.Text   = "Update User";
            lblUserID.Text = _User.UserID.ToString();

        }
        
       
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = true;
        }
        
        private void AllTextBoxes_Validating(object sender, CancelEventArgs e)
        {
            TextBox CurrentTextBox = (TextBox)sender;


            switch (CurrentTextBox.Name.ToString())
            {
                case "txtUserName":
                {

                        if (string.IsNullOrEmpty(CurrentTextBox.Text))
                        {
                            //CurrentTextBox.Focus();
                            e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "UserName should have a Value!");
                            
                        }
                        else if (clsUser.IsUserExists(CurrentTextBox.Text) && txtUserName.Text != _User.UserName.ToString())
                        {
                            //CurrentTextBox.Focus();
                            e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "Username is already in use, please use another username");
                            
                        }
                        else
                        {
                            e.Cancel = false;
                            errorProvider1.SetError(CurrentTextBox, "");

                        }

                        

                        break;
                }
                case "txtPassword":
                {
                        if (string.IsNullOrEmpty(CurrentTextBox.Text))
                        {
                            //CurrentTextBox.Focus();
                            e.Cancel = true;
                            
                            errorProvider1.SetError(CurrentTextBox, "Password should have a Value!");
                            
                        }
                        else
                        {
                            e.Cancel = false;
                            errorProvider1.SetError(CurrentTextBox, "");

                        }



                        break;
                }
                case "txtConfirmPassword":
                {
                        if (string.IsNullOrEmpty(CurrentTextBox.Text))
                        {

                            CurrentTextBox.Focus();
                            e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "ConfirmPassword should have a Value!");
                            
                        }
                        else if (txtConfirmPassword.Text != txtPassword.Text)
                        {
                            CurrentTextBox.Focus();
                            e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "password confirmation does not match password!");
                            

                        }
                        else
                        {
                            e.Cancel = false;
                            errorProvider1.SetError(CurrentTextBox, "");

                        }



                        break;
                }

            }



            
            


        }
        
        private void btnShowHidePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*' && txtConfirmPassword.PasswordChar == '*')
            {
                btnShowHidePassword.Image = Resources.show;
                txtPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConfirmPassword.PasswordChar = '*';
                btnShowHidePassword.Image = Resources.close_eye;

            }
        }
        
    
    }
}
