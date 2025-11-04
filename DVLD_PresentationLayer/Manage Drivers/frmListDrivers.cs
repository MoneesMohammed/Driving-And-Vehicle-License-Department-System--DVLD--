using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.International_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Manage_Drivers
{
    public partial class frmListDrivers : Form
    {

        private DataTable _dtAllDrivers;


        public frmListDrivers()
        {
            InitializeComponent();
        }


        private void _RefreshDriversList()
        {
            _dtAllDrivers = clsDriver.GetAllDriver();
            dgvAllDrivers.DataSource = _dtAllDrivers;

            _FormatDGV();

            lblRecodes.Text = dgvAllDrivers.Rows.Count.ToString();

        }

        private void _FormatDGV()
        {
            if (dgvAllDrivers.Columns.Count <= 0)
                return;

            dgvAllDrivers.Columns[0].HeaderText = "Driver ID";
            dgvAllDrivers.Columns[0].Width = 30;

            dgvAllDrivers.Columns[1].HeaderText = "Person ID";
            dgvAllDrivers.Columns[1].Width = 30;

            dgvAllDrivers.Columns[2].HeaderText = "National No.";
            dgvAllDrivers.Columns[2].Width = 30;

            dgvAllDrivers.Columns[3].HeaderText = "Full Name";
            dgvAllDrivers.Columns[3].Width = 80;

            dgvAllDrivers.Columns[4].HeaderText = "Date";
            dgvAllDrivers.Columns[4].Width = 40;

            dgvAllDrivers.Columns[5].HeaderText = "Active Licenses";
            dgvAllDrivers.Columns[5].Width = 250;
            

        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _RefreshDriversList();
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
                _RefreshDriversList();
            }
            else
            {
                txtFilterBy.Visible = true;
                txtFilterBy.Focus();
            }


        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
           
            string FilterColumn = cbFilterBy.Text;

            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecodes.Text = dgvAllDrivers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim()); //[FilterColumn] = txtFilterBy.Text
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());
            //[FilterColumn] LIKE 'txtFilterBy.Text%'

            lblRecodes.Text = dgvAllDrivers.Rows.Count.ToString();


        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }

        }

        private void tsmShowPersonInfo_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvAllDrivers.CurrentRow.Cells[1].Value;

            frmShowDetailsPerson frmShowDetailsPerson = new frmShowDetailsPerson(PersonID);
            frmShowDetailsPerson.ShowDialog();
        }

        private void tsmIssueInternationalLicense_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }


        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {

            int PersonID = (int)dgvAllDrivers.CurrentRow.Cells[1].Value;
            frmLicenseHistory LicenseHistory = new frmLicenseHistory(PersonID);
            LicenseHistory.ShowDialog();
        }

        
    }
}
