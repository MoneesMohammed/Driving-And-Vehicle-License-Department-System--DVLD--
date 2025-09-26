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
        public clsUser User;
        private int _UserID;

        public ucUserDetails()
        {
            InitializeComponent();
            
        }

        public ucUserDetails(int UserID)
        {
            InitializeComponent();

            this._UserID = UserID;

        }

        private void ucUserDetails_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoaducPersonDetails()
        {
            ucPersonDetails1.LoadPersonInfo(User.PersonID);
        }

        public void RefreshUCUserDetails(int UserID)
        {
            if (UserID == -1)
                return;

            User = clsUser.Find(UserID);

            if (User == null)
                return;


            _LoaducPersonDetails();

            lblUserID.Text  = User.UserID.ToString();
            lblUserName.Text = User.UserName;
            lblIsActive.Text = User.IsActive ? "Yes" : "No";

        }

        private void _LoadData()
        {

            User = clsUser.Find(_UserID);

            if (User == null)
                return;

            
            _LoaducPersonDetails();

            lblUserID.Text   = User.UserID.ToString();
            lblUserName.Text = User.UserName;
            lblIsActive.Text = User.IsActive ? "Yes" : "No";

        }

    }
}
