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
        private int _LDLAppID= -1;

        public frmApplicationDetails(int LDLAppID)
        {
            InitializeComponent();

            _LDLAppID = LDLAppID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmApplicationDetails_Load(object sender, EventArgs e)
        {
            ucDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDLAppID(_LDLAppID);
        }
    }
}
