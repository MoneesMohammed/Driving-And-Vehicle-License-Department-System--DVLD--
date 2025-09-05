using Driving___Vehicle_License_Department__DVLD_.Properties;
using Driving___Vehicle_License_Department__DVLD_.UserControls;
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

        private int _UserID;
        
        private clsUser _User;
        private clsPerson _Person;



       public frmAddNewUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;

            if (UserID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            _LoadData();


        }

       

        private void _LoadData()
        {

            btnSave.Enabled = false;

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New User";
                _User = new clsUser();
                return;
            }

            lblMode.Text = "Update User";

           
            _User   = clsUser.Find(_UserID);
            

            if (_User == null)
            {
                MessageBox.Show($"this form will be closed because no User with ID Found {_UserID}");
                this.Close();
                return;
            }

            ucFilterPerson1.LoadDataForUpdateMode(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();

            txtUserName.Text        = _User.UserName;
            txtPassword.Text        = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            cbIsActive.Checked      = _User.IsActive;

            if (frmLogin.user.UserID != _UserID)
            {
                btnShowHidePassword.Enabled = false;
                txtUserName.Enabled = false;
          
            }
            else
            {
                cbIsActive.Enabled = false;
            }

            txtPassword.Enabled = false;
            txtConfirmPassword.Enabled = false;
           

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            
            tabControl1.Selecting -= tabControl1_Selecting;  // Temporarily unblock
            tabControl1.SelectedTab = tpPersonalInfo;        // or any other tab
            tabControl1.Selecting += tabControl1_Selecting;  // Reblock
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _Person = ucFilterPerson1.Person;

            if (_Mode == enMode.Update)
            {
                GoToTabLoginInfo();
                return;
            }


            if (_Person == null || _Person.PersonID == -1)
            {
                MessageBox.Show($"There is no person.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (clsUser.IsUserExistsByPersonID(_Person.PersonID))
            {
                MessageBox.Show("Selected person already has used Choose another one", "Select another person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            GoToTabLoginInfo();
        }

        private void GoToTabLoginInfo()
        {
            tabControl1.Selecting -= tabControl1_Selecting;  // Temporarily unblock
            tabControl1.SelectedTab = tpLoginInfo;           // or any other tab
            tabControl1.Selecting += tabControl1_Selecting;  // Reblock

        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab != tpLoginInfo)
                return;

            
            _User.PersonID = _Person.PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = cbIsActive.Checked;

            if (_User.Save())
            {
                MessageBox.Show("Data saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error : data is not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _Mode = enMode.Update;

            lblMode.Text = "Update User";
            lblUserID.Text = _User.UserID.ToString();

        }

        private void AllTextBoxes_TextChanged(object sender, EventArgs e)
        {
            bool OneOfIsEmpty = string.IsNullOrWhiteSpace(txtUserName.Text) ||
                                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                                string.IsNullOrWhiteSpace(txtConfirmPassword.Text);

            if (OneOfIsEmpty)
            {
                btnSave.Enabled = false;
            }
            else if (txtConfirmPassword.Text != txtPassword.Text)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }

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
                        else if (clsUser.IsUserExistsByUserName(CurrentTextBox.Text) && txtUserName.Text != _User.UserName.ToString())
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
