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
        private DataTable _dtDriverLocalLicensesHistory;

        private DataTable _dtDriverInternationalLicensesHistory;

        private clsDriver _Driver;
        private int _DriverID = -1;
        public int DriverID { get { return _DriverID; } }

        public ucDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicensesInfo()
        {
            _dtDriverLocalLicensesHistory = clsDriver.GetLicenses(_DriverID);
            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;
            lblRecodesLocal.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count <= 0)
                return;

            dgvLocalLicensesHistory.Columns[0].Width = 100;
            dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";

            dgvLocalLicensesHistory.Columns[1].Width = 100;
            dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";

            dgvLocalLicensesHistory.Columns[2].Width = 200;
            dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";

            dgvLocalLicensesHistory.Columns[3].Width = 100;
            dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";

            dgvLocalLicensesHistory.Columns[4].Width = 100;
            dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";

            dgvLocalLicensesHistory.Columns[5].Width = 100;
            dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";


        }


        private void _LoadInternationalLicensesInfo()
        {
            _dtDriverInternationalLicensesHistory = clsDriver.GetInternationalLicenses(_DriverID);

            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicensesHistory;
            lblRecodes.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                //dgvInternationalLicensesHistory.Columns[0].Width = 100;
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";

                //dgvInternationalLicensesHistory.Columns[1].Width = 30;
                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";

                //dgvInternationalLicensesHistory.Columns[2].Width = 30;
                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";

                //dgvInternationalLicensesHistory.Columns[3].Width = 80;
                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";

                //dgvInternationalLicensesHistory.Columns[4].Width = 80;
                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";

                //dgvInternationalLicensesHistory.Columns[5].Width = 20;
                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";

            }

        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;

            _Driver = clsDriver.FindByDriverID(_DriverID);

            if (_Driver == null)
            {
                MessageBox.Show("There Is No Driver With ID = "+ _DriverID, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
              
            _LoadLocalLicensesInfo();
            _LoadInternationalLicensesInfo();

        }

        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("There Is No Driver Linked With Person With ID = " + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DriverID = _Driver.DriverID;


            _LoadLocalLicensesInfo();
            _LoadInternationalLicensesInfo();

        }

        public void Clear()
        {
            _dtDriverLocalLicensesHistory.Clear();

            _dtDriverInternationalLicensesHistory.Clear();



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
                frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(LicenseID);
                frmLicenseInfo.ShowDialog();

            }

            
        }


    }
}
