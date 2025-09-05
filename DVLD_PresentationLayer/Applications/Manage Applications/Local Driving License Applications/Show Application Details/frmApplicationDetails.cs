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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Application_Details
{
    public partial class frmApplicationDetails : Form
    {
        int LDLAppID;

        public frmApplicationDetails(int LDLAppID)
        {
            InitializeComponent();

            this.LDLAppID = LDLAppID;
            
        }

        private void frmApplicationDetails_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {
            ucDrivingLicenseApplicationInfo1.RefreshUcDrivingLicenseApplicationInfo(this.LDLAppID);

        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
