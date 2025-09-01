using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.International_License.International_Driver_Info;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
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

namespace Driving___Vehicle_License_Department__DVLD_.UserControls
{
    public partial class ucDriverLicenses : UserControl
    {
        public ucDriverLicenses()
        {
            InitializeComponent();
        }

        public void RefreshUCDriverLicenses(int PersonID)
        {
            DataTable LocalLicensesHistory = clsLicenses.GetAllLicenseByPersonID(PersonID);
            dgvLocalLicensesHistory.DataSource =  LocalLicensesHistory;

            DataTable InternationalLicensesHistory = clsInternationalLicense.GetAllInternationalLicensesByPersonID(PersonID);
            dgvInternationalLicensesHistory.DataSource = InternationalLicensesHistory;

            _AdjustSizeDGV();
            lblRecodesLocal.Text = LocalLicensesHistory.Rows.Count.ToString();
            lblRecodes.Text = InternationalLicensesHistory.Rows.Count.ToString();
        }

        private void _AdjustSizeDGV()
        {
            
            if (dgvLocalLicensesHistory.Rows.Count <= 0)
                return;

            dgvLocalLicensesHistory.Columns[0].Width = 100;
            dgvLocalLicensesHistory.Columns[1].Width = 100;
            dgvLocalLicensesHistory.Columns[2].Width = 200;
            dgvLocalLicensesHistory.Columns[3].Width = 100;
            dgvLocalLicensesHistory.Columns[4].Width = 100;
            dgvLocalLicensesHistory.Columns[5].Width = 100;
            

        }

        private void tsmShowLicenseInfo_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tpInternational)
            {
                int Int_LicenseID = (int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value;

                frmInternationalDriverInfo frmInternationalDriverInfo = new frmInternationalDriverInfo(Int_LicenseID);
                frmInternationalDriverInfo.ShowDialog();

            }
            else
            {
                int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
                frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(LicenseID, false);
                frmLicenseInfo.ShowDialog();

            }



        }


    }
}
