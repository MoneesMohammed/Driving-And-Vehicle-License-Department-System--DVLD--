using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.Local_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Issue_Driving_License_The_First_Time;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Application_Details;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
using Driving___Vehicle_License_Department__DVLD_.Tests;
using DVLD_BusinessLayer;
using DVLD_DataAccessLayar;
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
using static Driving___Vehicle_License_Department__DVLD_.frmManageUsers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        DataTable _dtLocalDLApplications ;

        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _RefreshLocalDrivingLicenseApplicationsList()
        {
            _dtLocalDLApplications = clsLocalDrivingLicenseApplication.GetAllLocalDLApplications();
            dgvAllLocalDLApplications.DataSource = _dtLocalDLApplications;

            _FormatDGV();

            lblRecodes.Text = dgvAllLocalDLApplications.Rows.Count.ToString();

        }

        private void _FormatDGV()
        {
            if (dgvAllLocalDLApplications.Columns.Count < 0)
                return;

            dgvAllLocalDLApplications.Columns["L.D.L.AppID"].Width = 100;
            dgvAllLocalDLApplications.Columns["Driving Class"].Width = 200;
            dgvAllLocalDLApplications.Columns["National No."].Width = 100;
            dgvAllLocalDLApplications.Columns["Full Name"].Width = 260;
            dgvAllLocalDLApplications.Columns["Application Date"].Width = 150;
            dgvAllLocalDLApplications.Columns["Passed Tests"].Width = 100;
            dgvAllLocalDLApplications.Columns["Status"].Width = 100;

        }


        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshLocalDrivingLicenseApplicationsList();
            cbFilterBy.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocalDrivingLicenseApplications_SizeChanged(object sender, EventArgs e)
        {
            //size Form =  1292, 842
            //size dgv = 1242, 335 / 1242 - 1292 = 50 // 842 - 335 = 507

            //1107, 196

            dgvAllLocalDLApplications.Size = new System.Drawing.Size(this.Size.Width - 50, this.Size.Height - 507);
            dgvAllLocalDLApplications.Location = new System.Drawing.Point(12, 355);
            
            btnClose.Location = new System.Drawing.Point(this.Size.Width - 171, this.Size.Height - 100);

            pictureBox1.Location = new System.Drawing.Point((this.Size.Width / 2) - 108, 12);

            label1.Location = new System.Drawing.Point((this.Size.Width / 2) - 212, 242);

            label2.Location = new System.Drawing.Point(18, this.Size.Height - 114);

            lblRecodes.Location = new System.Drawing.Point(155, this.Size.Height - 114);

            btnAddNewLDLApp.Location = new System.Drawing.Point(this.Size.Width - 185, 196);

        }

        
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (cbFilterBy.Text == "None")
            {
                txtFilterBy.Visible = false;
                
                _RefreshLocalDrivingLicenseApplicationsList();
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
                _dtLocalDLApplications.DefaultView.RowFilter = "";
                lblRecodes.Text = dgvAllLocalDLApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "L.D.L.AppID" )
                _dtLocalDLApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim()); //[FilterColumn] = txtFilterBy.Text
            else
                _dtLocalDLApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());
            //[FilterColumn] LIKE 'txtFilterBy.Text%'

            lblRecodes.Text = dgvAllLocalDLApplications.Rows.Count.ToString();

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }

        }



        private void btnAddNewLDLApp_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicense = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicense.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void tsmShowApplicationDetails_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            frmApplicationDetails frmApplicationDetails = new frmApplicationDetails(ID);
            frmApplicationDetails.ShowDialog();

        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int LocalDLApplicationID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication(LocalDLApplicationID);
            frm.ShowDialog();
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            var result = MessageBox.Show($"Are you sure you want to delete \nthe Application by Local Driving License Application ID: {ID}", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result != DialogResult.OK)
                return;

            clsLocalDrivingLicenseApplication LocalDrivingLicense = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(ID);

            if (LocalDrivingLicense.GetPassedTestCount() != 0)
            {
                MessageBox.Show("You Cannot Delete This Application Because It Has Process.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LocalDrivingLicense != null)
            {
                if (LocalDrivingLicense.Delete())
                {

                    MessageBox.Show("Application has been deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshLocalDrivingLicenseApplicationsList();
                }
                else
                {
                    MessageBox.Show("Application was not deleted because it has data linked to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        private void tsmCancelApplication_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are you sure you want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result != DialogResult.Yes)
                return;


            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicense = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(ID);

            if (LocalDrivingLicense.ApplicationStatus == clsApplication.enApplicationStatus.Cancelled )
            {
                MessageBox.Show("Already Cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (LocalDrivingLicense.ApplicationStatus == clsApplication.enApplicationStatus.Completed)
            {
                MessageBox.Show("He Passed All The Tests.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
               
           
            if (LocalDrivingLicense != null)
            {
                if (LocalDrivingLicense.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    _RefreshLocalDrivingLicenseApplicationsList();
                }
                else
                    MessageBox.Show("Application Cancel failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }




       

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicense = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(ID);
            
            bool LicenseExists = LocalDrivingLicense.IsLicenseIssued();

            tsmIssueDrivingLicense_FirstTime.Enabled = (LocalDrivingLicense.ApplicationStatus == clsApplication.enApplicationStatus.New && !LicenseExists && LocalDrivingLicense.PassedAllTests());
            tsmShowLicense.Enabled                   = LicenseExists;
            tsmEdit.Enabled                          = !LicenseExists && (LocalDrivingLicense.ApplicationStatus == clsApplication.enApplicationStatus.New);
            tsmScheduleTests.Enabled                 = !LicenseExists;

            tsmCancelApplication.Enabled             = !LicenseExists && (LocalDrivingLicense.ApplicationStatus == clsApplication.enApplicationStatus.New);
            tsmDelete.Enabled                        = !LicenseExists && (LocalDrivingLicense.ApplicationStatus == clsApplication.enApplicationStatus.New);


            bool PassedVisionTest   =  LocalDrivingLicense.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest  =  LocalDrivingLicense.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest   =  LocalDrivingLicense.DoesPassTestType(clsTestType.enTestType.StreetTest);

            
            
            tsmScheduleTests.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicense.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (tsmScheduleTests.Enabled)
            {
                tsmiScheduleVisionTest.Enabled  = !PassedVisionTest;
                tsmiScheduleWrittenTest.Enabled = PassedVisionTest && !PassedWrittenTest;
                tsmiScheduleStreetTest.Enabled  = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }

        }

        private void tsmScheduleTests_DropDownOpening(object sender, EventArgs e)
        {
            //int TotalPassedTests = (int)dgvAllLocalDLApplications.CurrentRow.Cells[5].Value;

            //if (TotalPassedTests == 0)
            //{

            //    tsmiScheduleVisionTest .Enabled = true;
            //    tsmiScheduleWrittenTest.Enabled = false;
            //    tsmiScheduleStreetTest .Enabled = false;

            //}
            //else if (TotalPassedTests == 1)
            //{

            //    tsmiScheduleVisionTest.Enabled = false;
            //    tsmiScheduleWrittenTest.Enabled = true;
            //    tsmiScheduleStreetTest.Enabled = false;
            //}
            //else if (TotalPassedTests == 2)
            //{

            //    tsmiScheduleVisionTest.Enabled = false;
            //    tsmiScheduleWrittenTest.Enabled = false;
            //    tsmiScheduleStreetTest.Enabled = true;

            //}


        }


        private void tsmiScheduleVisionTest_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            
            frmListTestAppointment frm = new frmListTestAppointment(ID, clsTestType.enTestType.VisionTest);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void tsmiScheduleWrittenTest_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;
            

            frmListTestAppointment frm = new frmListTestAppointment(ID, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void tsmiScheduleStreetTest_Click(object sender, EventArgs e)
        {

            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;
            

            frmListTestAppointment frm = new frmListTestAppointment(ID, clsTestType.enTestType.StreetTest);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplicationsList();

        }

        private void tsmIssueDrivingLicense_FirstTime_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;
            frmIssueLicense_FirstTime issueDrivingLicense_FirstTime = new frmIssueLicense_FirstTime(ID);

            issueDrivingLicense_FirstTime.ShowDialog();


            _RefreshLocalDrivingLicenseApplicationsList();

        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            int ApplicationID = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(ID).ApplicationID;

            int LicenseID = clsLicense.FindByApplicationID(ApplicationID).LicenseID;

            frmLicenseInfo LicenseInfo = new frmLicenseInfo(LicenseID);
            LicenseInfo.ShowDialog();

            //_RefreshLocalDrivingLicenseApplicationsList();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            int PersonID = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(ID).ApplicantPersonID;

            frmLicenseHistory LicenseHistory = new frmLicenseHistory(PersonID);
            LicenseHistory.ShowDialog();

        }

        
    }
}
