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
using static Driving___Vehicle_License_Department__DVLD_.UserControls.ucFilterDriverLicenseInfo;

namespace Driving___Vehicle_License_Department__DVLD_.UserControls
{
    public partial class ucFilterDriverLicenseInfo : UserControl
    {
        public delegate void DataFoundEventHandler(object sender, clsLicenses License);

        public event DataFoundEventHandler DataFound;
        


        public clsLicenses License = new clsLicenses();

        public ucFilterDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void _RefreshUCDriverLicenseInfo(int LicenseID = -1)
        {

            ucDriverLicenseInfo1.RefreshUCDriverLicenseInfoByLicenseID(LicenseID);

            if(LicenseID == -1)
                License = new clsLicenses();
        }

        public void LoadDataForUpdateMode(int LicenseID)
        {
            
            License = clsLicenses.Find(LicenseID);

            if (License == null)
                return;


            gbFilter.Enabled = false;
            txtFilterByID.Text = License.LicenseID.ToString();

            DataFound?.Invoke(this, License);
            _RefreshUCDriverLicenseInfo(LicenseID);


        }

        private void btnSearchLicense_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            if (txtFilterByID.Text == string.Empty)
            {
                _RefreshUCDriverLicenseInfo();

                License = new clsLicenses();
                DataFound?.Invoke(this, License);

                MessageBox.Show("How do I search for a License without their information?\nThe box is empty!\nPlease fill in the appropriate information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            if (int.TryParse(txtFilterByID.Text, out int ID))
                License = clsLicenses.Find(ID);

            if (License == null)
            {
                _RefreshUCDriverLicenseInfo();

                License = new clsLicenses();
                DataFound?.Invoke(this, License);

                MessageBox.Show($"Don't Found License by ID = {txtFilterByID.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataFound?.Invoke(this, License);

            _RefreshUCDriverLicenseInfo(License.LicenseID);
        }

        private void txtFilterByID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                DoSearch(); 
            }
        }






    }
}
