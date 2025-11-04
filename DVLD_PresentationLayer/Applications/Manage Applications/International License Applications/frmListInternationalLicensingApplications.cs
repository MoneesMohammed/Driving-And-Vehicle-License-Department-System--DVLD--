using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.International_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.International_License.International_Driver_Info;
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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.International_License_Applications
{
    public partial class frmListInternationalLicensingApplications : Form
    {

        private DataTable _dtInternationalDLicense;

        public frmListInternationalLicensingApplications()
        {
            InitializeComponent();
        }


        private void _RefreshInternationalDrivingLicenseApplicationsList()
        {
            _dtInternationalDLicense = clsInternationalLicense.GetAllInternationalLicenses();
            dgvAllIntDLicenses.DataSource = _dtInternationalDLicense;

            _FormatDGV();

            lblRecodes.Text = dgvAllIntDLicenses.Rows.Count.ToString();

        }

        private void _FormatDGV()
        {
            if (dgvAllIntDLicenses.Columns.Count <= 0)
                return;

            dgvAllIntDLicenses.Columns[0].Width = 100;
            dgvAllIntDLicenses.Columns[1].Width = 100;
            dgvAllIntDLicenses.Columns[2].Width = 100;
            dgvAllIntDLicenses.Columns[3].Width = 100;
            dgvAllIntDLicenses.Columns[4].Width = 100;
            dgvAllIntDLicenses.Columns[5].Width = 100;
            dgvAllIntDLicenses.Columns[6].Width = 100;


        }

        private void frmListInternationalLicensingApplications_Load(object sender, EventArgs e)
        {

            cbFilterBy.SelectedIndex = 0;
            _RefreshInternationalDrivingLicenseApplicationsList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewIntDLApp_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frmNewInternationalLicenseApplication = new frmNewInternationalLicenseApplication();
            frmNewInternationalLicenseApplication.ShowDialog();

            _RefreshInternationalDrivingLicenseApplicationsList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (cbFilterBy.Text == "None")
            {
                txtFilterBy.Visible = false;
                cbIsActive.Visible = false;
                _RefreshInternationalDrivingLicenseApplicationsList();
            }
            else if (cbFilterBy.Text == "Is Active")
            {
                cbIsActive.SelectedIndex = 0;

                txtFilterBy.Visible = false;
                cbIsActive.Visible = true;
            }
            else
            {
                txtFilterBy.Visible = true;
                cbIsActive.Visible = false;
                txtFilterBy.Focus();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsActive.Text == "All")
            {
                _RefreshInternationalDrivingLicenseApplicationsList();
                return;
            }

            string FilterColumn = (cbIsActive.Text == "Yes") ? "1" : "0";

            _dtInternationalDLicense.DefaultView.RowFilter = string.Format($"[Is Active] = {FilterColumn}");

            lblRecodes.Text = dgvAllIntDLicenses.Rows.Count.ToString();
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = cbFilterBy.Text;

            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalDLicense.DefaultView.RowFilter = "";
                lblRecodes.Text = dgvAllIntDLicenses.Rows.Count.ToString();
                return;
            }

           _dtInternationalDLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim()); //[FilterColumn] = txtFilterBy.Text
           
           lblRecodes.Text = dgvAllIntDLicenses.Rows.Count.ToString();

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void tsmShowPersonDetails_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllIntDLicenses.CurrentRow.Cells[0].Value;
            int PersonID = clsInternationalLicense.Find(ID).ApplicantPersonID;

            frmShowDetailsPerson frmShowDetailsPerson = new frmShowDetailsPerson(PersonID);
            frmShowDetailsPerson.ShowDialog();

        }

        private void tsmShowLicenseDetails_Click(object sender, EventArgs e)
        {
            int Int_LicenseID = (int)dgvAllIntDLicenses.CurrentRow.Cells[0].Value;
            frmInternationalDriverInfo frmInternationalDriverInfo = new frmInternationalDriverInfo(Int_LicenseID);
            frmInternationalDriverInfo.ShowDialog();


        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllIntDLicenses.CurrentRow.Cells[0].Value;

            int PersonID = clsInternationalLicense.Find(ID).ApplicantPersonID;

            frmLicenseHistory LicenseHistory = new frmLicenseHistory(PersonID);
            LicenseHistory.ShowDialog();


        }

        
    }



}
