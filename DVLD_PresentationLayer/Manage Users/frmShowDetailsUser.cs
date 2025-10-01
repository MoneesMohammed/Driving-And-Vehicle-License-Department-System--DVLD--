using Driving___Vehicle_License_Department__DVLD_.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Manage_Users
{
    public partial class frmShowDetailsUser : Form
    {
        private int _UserID = -1;
       
        public frmShowDetailsUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void frmShowDetailsUser_Load(object sender, EventArgs e)
        {
            ucUserDetails1.LoadUserInfo(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
