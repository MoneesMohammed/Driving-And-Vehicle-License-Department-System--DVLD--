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

namespace Driving___Vehicle_License_Department__DVLD_
{
    public partial class ucUserDetails : UserControl
    {
        private clsUser _User;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }

        public ucUserDetails()
        {
            InitializeComponent();
            
        }

        
        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.FindByUserID(UserID);

            if (_User == null)
            {
                ResetUserInfo();
                MessageBox.Show($"No User With UserID ={UserID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();

        }

        private void ResetUserInfo()
        {
            _UserID          = -1;
            lblUserID.Text   = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";

        }

        private void _FillUserInfo()
        {
            _UserID = _User.UserID;

            ucPersonDetails1.LoadPersonInfo(_User.PersonID);

            lblUserID.Text   = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";

        }


    }
}
