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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Detain_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _RefreshListDetainedLicenses()
        {
            DataTable DetainedLicensesDataTable = clsDetainedLicense.GetAllDetainedLicenses_1();
            dgvAllDetainedLicenses.DataSource = DetainedLicensesDataTable;

            _AdjustSizeDGV();

            lblRecodes.Text = dgvAllDetainedLicenses.Rows.Count.ToString();

        }

        private void _AdjustSizeDGV()
        {
            if (dgvAllDetainedLicenses.Columns.Count <= 0)
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


        public enum enIsReleased { All, Yes, No }
        private enIsReleased _enIsReleased = enIsReleased.All;

        public enum enFilterBy { None, DetainID, IsReleased, NationalNo, FullName, ReleaseApplicationID }

        private enFilterBy _enFilterBy = enFilterBy.None;


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _enFilterBy = (enFilterBy)cbFilterBy.SelectedIndex;

            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (_enFilterBy == enFilterBy.None)
            {
                txtFilterBy.Visible = false;
                cbIsReleased.Visible = false;
                _RefreshListDetainedLicenses();
            }
            else if (_enFilterBy == enFilterBy.IsReleased)
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
            _enIsReleased = (enIsReleased)cbIsReleased.SelectedIndex;

            DataTable dt = clsDetainedLicense.GetAllDetainedLicenses_1();
            DataRow[] ResultRows = new DataRow[0];

            if (_enIsReleased == enIsReleased.Yes)
            {
                FilterBy(dt, ResultRows, "[Is Released] = 1");
            }
            else if (_enIsReleased == enIsReleased.No)
            {
                FilterBy(dt, ResultRows, "[Is Released] = 0");
            }
            else
            {
                _RefreshListDetainedLicenses();
            }


        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = clsDetainedLicense.GetAllDetainedLicenses_1();
            DataRow[] ResultRows = new DataRow[0];

            if (txtFilterBy.Text != "")
            {
                switch (_enFilterBy)
                {
                    case enFilterBy.DetainID:
                        {
                            FilterBy(dt, ResultRows, $"[Detain ID] = {txtFilterBy.Text} ");

                            break;
                        }
                    case enFilterBy.NationalNo:
                        {

                            FilterBy(dt, ResultRows, $"[National No.] = '{txtFilterBy.Text}' ");

                            break;
                        }
                    case enFilterBy.FullName:
                        {
                            FilterBy(dt, "Full Name");

                            break;
                        }
                    case enFilterBy.ReleaseApplicationID:
                        {
                            FilterBy(dt, "Release Application ID");

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
                dgvAllDetainedLicenses.DataSource = ResultRows.CopyToDataTable();
                _AdjustSizeDGV();
                lblRecodes.Text = ResultRows.Count().ToString();

            }
            else
            {
                dgvAllDetainedLicenses.DataSource = null;

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

                    //break;
                }
                else
                {
                    dgvAllDetainedLicenses.DataSource = null;

                    lblRecodes.Text = "0";
                }

            }


            dgvAllDetainedLicenses.DataSource = filteredTable;
            _AdjustSizeDGV();
            lblRecodes.Text = filteredTable.Rows.Count.ToString();


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
            int ID = (int)dgvAllDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicenses.Find(ID).Application.ApplicantPersonID;

            frmShowDetailsPerson frmShowDetailsPerson = new frmShowDetailsPerson(PersonID);
            frmShowDetailsPerson.ShowDialog();
        }

        private void tsmShowLicenseDetails_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllDetainedLicenses.CurrentRow.Cells[1].Value;
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(ID,false);
            frmLicenseInfo.ShowDialog();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicenses.Find(ID).Application.ApplicantPersonID;

            frmLicenseHistory LicenseHistory = new frmLicenseHistory(PersonID);
            LicenseHistory.ShowDialog();
        }

        private void tsmReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllDetainedLicenses.CurrentRow.Cells[1].Value;

            frmReleaseDetainedLicense frmReleaseDetainedLicense = new frmReleaseDetainedLicense(ID);
            frmReleaseDetainedLicense.ShowDialog();

            _RefreshListDetainedLicenses();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int ID = (int)dgvAllDetainedLicenses.CurrentRow.Cells[0].Value;

            if(clsDetainedLicense.Find(ID).IsReleased)
               tsmReleaseDetainedLicense.Enabled = false;
            else
               tsmReleaseDetainedLicense.Enabled = true;


        }


    }
}
