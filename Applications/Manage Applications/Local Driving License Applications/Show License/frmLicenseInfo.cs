using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License
{
    public partial class frmLicenseInfo : Form
    {
        int ID;
        bool IsLDLAppID;

        public frmLicenseInfo(int ID , bool IsLDLAppID = true)
        {
            InitializeComponent();
            this.ID = ID;
            this.IsLDLAppID = IsLDLAppID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            if(IsLDLAppID)
              ucDriverLicenseInfo1.RefreshUCDriverLicenseInfo(ID);
            else
             ucDriverLicenseInfo1.RefreshUCDriverLicenseInfoByLicenseID(ID);

        }
    }
}
