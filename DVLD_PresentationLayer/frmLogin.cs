using Driving___Vehicle_License_Department__DVLD_.Properties;
using DVLD.Classes;
using DVLD_BusinessLayer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Driving___Vehicle_License_Department__DVLD_
{
    public partial class frmLogin : Form
    {
        private string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\YourSoftware\DVLD";

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnShowHidePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                btnShowHidePassword.Image = Resources.show;
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                btnShowHidePassword.Image = Resources.close_eye;

            }
                
        }

        private void _WriteUserNameAndPasswordOnRegistry(string Username, string Password)
        {
            try
            {
                Registry.SetValue(keyPath, "Username", Username, RegistryValueKind.String);
                Registry.SetValue(keyPath, "Password", Password, RegistryValueKind.String);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool _ReadUserNameAndPasswordOnRegistry(ref string Username, ref string Password)
        {
            try
            {
                Username = Registry.GetValue(keyPath, "Username", null) as string;
                Password = Registry.GetValue(keyPath, "Password", null) as string;

                if (Username == null || Password == null)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {

            clsUser user = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(), clsUtil.ComputeHash(txtPassword.Text.Trim()));


            if (user != null)
            {
                if (user.IsActive)
                {
                    if (cbRememberMe.Checked )
                    {
                        _WriteUserNameAndPasswordOnRegistry(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    }
                    else 
                    {
                        _WriteUserNameAndPasswordOnRegistry("","");
                    }

                    clsGlobal.CurrentUser = user;
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                else
                {
                    txtUserName.Focus();
                    MessageBox.Show("your account is not active it..\nplease contact your admin", "Warning Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid The Username/Password .", "Warning Credentials", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

            string Username = "" , Password = "" ;

            if (_ReadUserNameAndPasswordOnRegistry(ref Username,ref Password))
            {
                
               txtUserName.Text = Username;
               txtPassword.Text = Password;
               cbRememberMe.Checked = true;
            }
            else
            {
               cbRememberMe.Checked = false;
            }
        }


    }
}
