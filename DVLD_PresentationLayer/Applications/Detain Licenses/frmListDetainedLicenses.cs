using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
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
using System.Xml.Linq;
using static Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.International_License_Applications.frmListInternationalLicensingApplications;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Detain_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {

        private DataTable _dtDetainedLicenses;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _RefreshListDetainedLicenses()
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses_1();
            dgvAllDetainedLicenses.DataSource = _dtDetainedLicenses;

            _FormatDGV();

            lblRecodes.Text = dgvAllDetainedLicenses.Rows.Count.ToString();

        }

        private void _FormatDGV()
        {
            if (dgvAllDetainedLicenses.Rows.Count <= 0)
                return;

            dgvAllDetainedLicenses.Columns[0].Width = 100;
            dgvAllDetainedLicenses.Columns[1].Width = 100;
            dgvAllDetainedLicenses.Columns[2].Width = 150;
            dgvAllDetainedLicenses.Columns[3].Width = 100;
            dgvAllDetainedLicenses.Columns[4].Width = 100;
            dgvAllDetainedLicenses.Columns[5].Width = 150;
            dgvAllDetainedLicenses.Columns[6].Width = 100;
            dgvAllDetainedLicenses.Columns[7].Width = 250;
            dgvAllDetainedLicenses.Columns[8].Width = 100;

        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _RefreshListDetainedLicenses();
            cbFilterBy.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (cbFilterBy.Text == "None")
            {
                txtFilterBy.Visible = false;
                cbIsReleased.Visible = false;
                _RefreshListDetainedLicenses();
            }
            else if (cbFilterBy.Text == "Is Released")
            {
                cbIsReleased.SelectedIndex = 0;

                txtFilterBy.Visible = false;
                cbIsReleased.Visible = true;
            }
            else
            {
                txtFilterBy.Visible = true;
                cbIsReleased.Visible = false;
                txtFilterBy.Focus();
            }

        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsReleased.Text == "All")
            {
                _RefreshListDetainedLicenses();
                return;
            }

            string FilterColumn = (cbIsReleased.Text == "Yes") ? "1" : "0";

            _dtDetainedLicenses.DefaultView.RowFilter = string.Format($"[Is Released] = {FilterColumn}");

            lblRecodes.Text = dgvAllDetainedLicenses.Rows.Count.ToString();

        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = cbFilterBy.Text;

            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None" || FilterColumn == "Is Released")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblRecodes.Text = _dtDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "Release Application ID" || FilterColumn == "Detain ID")
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim()); //[FilterColumn] = txtFilterBy.Text
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());
            //[FilterColumn] LIKE 'txtFilterBy.Text%'

            lblRecodes.Text = dgvAllDetainedLicenses.Rows.Count.ToString();

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frmReleaseDetainedLicense = new frmReleaseDetainedLicense();
            frmReleaseDetainedLicense.ShowDialog();

            _RefreshListDetainedLicenses();
        }

        private void btnDetainedLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();

            _RefreshListDetainedLicenses();

        }

        private void tsmShowPersonDetails_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvAllDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;

            frmShowDetailsPerson frmShowDetailsPerson = new frmShowDetailsPerson(PersonID);
            frmShowDetailsPerson.ShowDialog();
        }

        private void tsmShowLicenseDetails_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvAllDetainedLicenses.CurrentRow.Cells[1].Value;
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(LicenseID);
            frmLicenseInfo.ShowDialog();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvAllDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;

            frmLicenseHistory LicenseHistory = new frmLicenseHistory(PersonID);
            LicenseHistory.ShowDialog();
        }

        private void tsmReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvAllDetainedLicenses.CurrentRow.Cells[1].Value;

            frmReleaseDetainedLicense frmReleaseDetainedLicense = new frmReleaseDetainedLicense(LicenseID);
            frmReleaseDetainedLicense.ShowDialog();

            _RefreshListDetainedLicenses();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
          tsmReleaseDetainedLicense.Enabled = !(bool)dgvAllDetainedLicenses.CurrentRow.Cells[3].Value;
        }

       
    }
}
