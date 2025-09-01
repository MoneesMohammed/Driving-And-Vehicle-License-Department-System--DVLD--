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
    public partial class frmUserInfo : Form
    {
        private int _UserID;
        private ucUserDetails _ucUserDetails;

        public frmUserInfo()
        {
            InitializeComponent();
        }


        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;

        }

        private void _LoaducUserDetails()
        {
            _ucUserDetails.Location = new Point(12, 12);
            this.Controls.Add(_ucUserDetails);
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {

            _LoadData();

        }


        private void _LoadData()
        {
            _ucUserDetails = new ucUserDetails(_UserID);
            _LoaducUserDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
