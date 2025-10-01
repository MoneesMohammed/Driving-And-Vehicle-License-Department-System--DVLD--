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
        private int _UserID ;


        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            _User = clsUser.FindByUserID(_UserID);

            if (_User == null)
            {
                MessageBox.Show($"Could not find user with id = ={_UserID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ucUserDetails1.LoadUserInfo(_UserID);

        }


        private void ResetDefaultValues()
        {
            txtCurrentPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtCurrentPassword.Focus();

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

                        if (string.IsNullOrEmpty(CurrentTextBox.Text.Trim()))
                        {
                            
                           // e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "CurrentPassword should have a Value!");

                        }
                        else if (txtCurrentPassword.Text.Trim() != _User.Password.Trim())
                        {
                            
                            e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "your current password is wrong");

                        }
                        else
                        {
                            
                            errorProvider1.SetError(CurrentTextBox, null);

                        }



                        break;
                    }
                case "txtNewPassword":
                    {
                        if (string.IsNullOrEmpty(CurrentTextBox.Text.Trim()))
                        {
                            
                            //e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "Password should have a Value!");

                        }
                        else
                        {
                            
                            errorProvider1.SetError(CurrentTextBox, null);

                        }



                        break;
                    }
                case "txtConfirmPassword":
                    {
                        if (string.IsNullOrEmpty(CurrentTextBox.Text.Trim()))
                        {

                            
                            //e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "ConfirmPassword should have a Value!");

                        }
                        else if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
                        {
                            
                            e.Cancel = true;
                            errorProvider1.SetError(CurrentTextBox, "password confirmation does not match password!");


                        }
                        else
                        {
                            
                            errorProvider1.SetError(CurrentTextBox, null);

                        }



                        break;
                    }

            }



        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _User.Password = txtNewPassword.Text;


            if (_User.Save())
            {
                ResetDefaultValues();
                MessageBox.Show("Password Changed successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error : data is not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }


}
