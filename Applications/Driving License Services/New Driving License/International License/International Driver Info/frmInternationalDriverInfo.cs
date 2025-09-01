using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.International_License.International_Driver_Info
{
    public partial class frmInternationalDriverInfo : Form
    {
       private int _Int_LicenseID;


        public frmInternationalDriverInfo(int Int_LicenseID)
        {
            InitializeComponent();

             _Int_LicenseID = Int_LicenseID;

        }

        private void frmInternationalDriverInfo_Load(object sender, EventArgs e)
        {
            ucDriverInternationalLicenseInfo1.RefreshUCDriverInternationalLicenseInfo(_Int_LicenseID);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
