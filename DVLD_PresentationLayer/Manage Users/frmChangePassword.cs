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

namespace Driving___Vehicle_License_Department__DVLD_
{
    public partial class frmChangePassword : Form
    {

        
        private clsUser _User  ;

        public frmChangePassword()
        {
            InitializeComponent();
        }

        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            _User = clsUser.Find(UserID);


        }

        private void _LoadUCUserDetails()
        {
            ucUserDetails1.RefreshUCUserDetails(_User.UserID);
            
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _LoadData();
            
        }

        private void _LoadData()
        {
            _LoadUCUserDetails();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AllTextBoxes_TextChanged(object sender, EventArgs e)
        {
            bool OneOfIsEmpty = string.IsNullOrWhiteSpace(txtCurrentPassword.Text) ||
                                string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
                                string.IsNullOrWhiteSpace(txtConfirmPassword.Text);

            if (OneOfIsEmpty)
            {
                btnSave.Enabled = false;
            }
            else if (txtCurrentPassword.Text != _User.Password)
            {
                btnSave.Enabled = false;
            }
            else if (txtConfirmPassword.Text != txtNewPassword.Text)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }

        private void AllTextBoxes_Validating(object sender, CancelEventArgs e)
        {


            TextBox CurrentTextBox = (TextBox)sender;


            switch (CurrentTextBox.Name.ToString())
            {
                case "txtCurrentPassword":
                    {

                        if (string.IsNullOrEmpty(CurrentTextBox.Text))
                        {
                            
                           // e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "CurrentPassword should have a Value!");

                        }
                        else if (txtCurrentPassword.Text != _User.Password)
                        {
                            
                            //e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "your current password is wrong");

                        }
                        else
                        {
                            //e.Cancel = false;
                            errorProvider1.SetError(CurrentTextBox, "");

                        }



                        break;
                    }
                case "txtNewPassword":
                    {
                        if (string.IsNullOrEmpty(CurrentTextBox.Text))
                        {
                            
                            //e.Cancel = true;

                            errorProvider1.SetError(CurrentTextBox, "Password should have a Value!");

                        }
                        else
                        {
                            //e.Cancel = false;
                            errorProvider1.SetError(CurrentTextBox, "");

                        }



                        break;
                    }
                case "txtConfirmPassword":
                    {
                        if (string.IsNullOrEmpty(CurrentTextBox.Text))
                        {

                            //CurrentTextBox.Focus();
                            //e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "ConfirmPassword should have a Value!");

                        }
                        else if (txtConfirmPassword.Text != txtNewPassword.Text)
                        {
                            //CurrentTextBox.Focus();
                           // e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "password confirmation does not match password!");


                        }
                        else
                        {
                            //e.Cancel = false;
                            errorProvider1.SetError(CurrentTextBox, "");

                        }



                        break;
                    }

            }



        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            _User.Password = txtNewPassword.Text;


            if (_User.Save())
            {
                MessageBox.Show("Data saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error : data is not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }


}
