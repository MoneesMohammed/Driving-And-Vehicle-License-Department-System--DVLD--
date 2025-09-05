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
        public frmListInternationalLicensingApplications()
        {
            InitializeComponent();
        }


        private void _RefreshInternationalDrivingLicenseApplicationsList()
        {
            DataTable InternationalDLicenseDataTable = clsInternationalLicense.GetAllInternationalLicenses_1();
            dgvAllIntDLicenses.DataSource = InternationalDLicenseDataTable;

            _AdjustSizeDGV();

            lblRecodes.Text = InternationalDLicenseDataTable.Rows.Count.ToString();

        }

        private void _AdjustSizeDGV()
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

            _RefreshInternationalDrivingLicenseApplicationsList();
            cbFilterBy.SelectedIndex = 0;
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

        public enum enIsActive { All, Yes, No }
        private enIsActive _enIsActive = enIsActive.All;


        public enum enFilterBy { None, IntDLAppID, ApplicationID, LicenseID, DriverID , IsActive }

        private enFilterBy _enFilterBy = enFilterBy.None;


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _enFilterBy = (enFilterBy)cbFilterBy.SelectedIndex;

            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (_enFilterBy == enFilterBy.None)
            {
                txtFilterBy.Visible = false;
                cbIsActive.Visible = false;
                _RefreshInternationalDrivingLicenseApplicationsList();
            }
            else if (_enFilterBy == enFilterBy.IsActive)
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
            _enIsActive = (enIsActive)cbIsActive.SelectedIndex;

            DataTable dt = clsInternationalLicense.GetAllInternationalLicenses_1();
            DataRow[] ResultRows = new DataRow[0];

            if (_enIsActive == enIsActive.Yes)
            {
                FilterBy(dt, ResultRows, "[Is Active] = 1");
            }
            else if (_enIsActive == enIsActive.No)
            {
                FilterBy(dt, ResultRows, "[Is Active] = 0");
            }
            else
            {
                _RefreshInternationalDrivingLicenseApplicationsList();
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = clsInternationalLicense.GetAllInternationalLicenses_1();
            DataRow[] ResultRows = new DataRow[0];

            if (txtFilterBy.Text != "")
            {
                switch (_enFilterBy)
                {
                    case enFilterBy.IntDLAppID:
                        {
                            FilterBy(dt, ResultRows, $"[Int.License ID] = {txtFilterBy.Text} ");

                            break;
                        }
                    case enFilterBy.ApplicationID:
                        {

                            FilterBy(dt, ResultRows, $"[Application ID] = {txtFilterBy.Text} ");

                            break;
                        }
                    case enFilterBy.LicenseID:
                        {
                            FilterBy(dt, ResultRows, $"[L.License ID] = {txtFilterBy.Text} ");

                            break;
                        }
                    case enFilterBy.DriverID:
                        {
                            FilterBy(dt, ResultRows, $"[Driver ID] = {txtFilterBy.Text} ");

                            break;
                        }
                }
            }
        }

        private void FilterBy(DataTable DataTable, DataRow[] ResultRows, string Select)
        {

            ResultRows = DataTable.Select(Select);

            if (ResultRows.Length > 0)
            {
                dgvAllIntDLicenses.DataSource = ResultRows.CopyToDataTable();
                _AdjustSizeDGV();
                lblRecodes.Text = ResultRows.Count().ToString();

            }
            else
            {
                dgvAllIntDLicenses.DataSource = null;

                lblRecodes.Text = "0";
            }


        }


        private void FilterBy(DataTable DataTable, string ColumnName)
        {
            DataTable filteredTable = DataTable.Clone();

            foreach (DataRow row in DataTable.Rows)
            {
                string value = row[ColumnName].ToString();

                if (value.ToUpper().Contains(txtFilterBy.Text.ToUpper()))
                {
                    filteredTable.ImportRow(row);

                    break;
                }
                else
                {
                    dgvAllIntDLicenses.DataSource = null;

                    lblRecodes.Text = "0";
                }

            }


            dgvAllIntDLicenses.DataSource = filteredTable;
            _AdjustSizeDGV();
            lblRecodes.Text = filteredTable.Rows.Count.ToString();


        }

        private void tsmShowPersonDetails_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllIntDLicenses.CurrentRow.Cells[0].Value;
            int PersonID = clsInternationalLicense.Find(ID).Application.ApplicantPersonID;

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

            int PersonID = clsInternationalLicense.Find(ID).Application.ApplicantPersonID;

            frmLicenseHistory LicenseHistory = new frmLicenseHistory(PersonID);
            LicenseHistory.ShowDialog();


        }
    }



}
