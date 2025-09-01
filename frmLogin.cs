using Driving___Vehicle_License_Department__DVLD_.Properties;
using DVLD_BusinessLayer;
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

        public static clsUser user = new clsUser();
        private string RememberMe = "";

        public frmLogin()
        {
            InitializeComponent();

            RememberMe = _ReadFiletxt();

            if (string.Empty != RememberMe && int.TryParse(RememberMe, out int ID))
            {
                user = clsUser.Find(ID);
                if (user != null)
                {
                    txtUserName.Text = user.UserName;
                    txtPassword.Text = user.Password;
                }

            }
            else
            {
                cbRememberMe.Checked = false;
            }


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

        private bool _IsAccountActivel()
        { 
            return user.IsActive;
        }

        private bool _UsernamePasswordVerificationSuccessful()
        {
            
            if (user == null || user.UserID == -1)
                return false;

            if(user.Password == txtPassword.Text)
                return true;


            return false;

        }

        private void _WriteOnFiletxt(string Write)
        {

            using (FileStream fs = File.Create("RememberMe.txt"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(Write);
                fs.Write(info, 0, info.Length);
            }

        }

        private string _ReadFiletxt()
        {
            using (StreamReader sr = File.OpenText("RememberMe.txt"))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {

                    //string[] str = s.Split('/');

                    return s;

                }

                return s;
            }
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {

            user = clsUser.Find(txtUserName.Text);


            if (_UsernamePasswordVerificationSuccessful())
            {
                if (_IsAccountActivel())
                {
                    if (cbRememberMe.Checked && RememberMe != user.UserID.ToString())
                    {
                        _WriteOnFiletxt(user.UserID.ToString());
                    }
                    else if(!cbRememberMe.Checked)
                    {
                        _WriteOnFiletxt("");
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                else
                {
                    MessageBox.Show("your account is not active it..\nplease contact your admin", "Warning Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Invalid The Username/Password .", "Warning Credentials", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            
        }
    }
}
