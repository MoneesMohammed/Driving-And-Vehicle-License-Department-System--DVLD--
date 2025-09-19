using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.Local_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Issue_Driving_License_The_First_Time;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Application_Details;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Street_Test_Appointments;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Written_Test_Appointments;
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
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _RefreshLocalDrivingLicenseApplicationsList()
        {
            DataTable LocalDLApplicationsDataTable = clsLocalDrivingLicenseApplication.GetAllLocalDLApplications_1();
            dgvAllLocalDLApplications.DataSource = LocalDLApplicationsDataTable;

            _AdjustSizeDGV();

            lblRecodes.Text = LocalDLApplicationsDataTable.Rows.Count.ToString();

        }

        private void _AdjustSizeDGV()
        {
            if (dgvAllLocalDLApplications.Columns.Count <= 0)
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

        public enum enFilterBy { None, LDLAppID, NationalNo, FullName, Status }
        
        private enFilterBy _enFilterBy = enFilterBy.None;

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            _enFilterBy = (enFilterBy)cbFilterBy.SelectedIndex;

            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (_enFilterBy == enFilterBy.None)
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
            DataTable dt = clsLocalDrivingLicenseApplication.GetAllLocalDLApplications_1();
            DataRow[] ResultRows = new DataRow[0];

            if (txtFilterBy.Text != "")
            {
                switch (_enFilterBy)
                {
                    case enFilterBy.LDLAppID:
                        {
                            FilterBy(dt, ResultRows, $"[L.D.L.AppID] = {txtFilterBy.Text} ");

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
                    case enFilterBy.Status:
                        {
                            FilterBy(dt, "Status");

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
                dgvAllLocalDLApplications.DataSource = ResultRows.CopyToDataTable();
                _AdjustSizeDGV();
                lblRecodes.Text = ResultRows.Count().ToString();

            }
            else
            {
                dgvAllLocalDLApplications.DataSource = null;

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
                    dgvAllLocalDLApplications.DataSource = null;

                    lblRecodes.Text = "0";
                }

            }


            dgvAllLocalDLApplications.DataSource = filteredTable;
            _AdjustSizeDGV();
            lblRecodes.Text = filteredTable.Rows.Count.ToString();


        }

        private void btnAddNewLDLApp_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicense = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicense.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void tsmCancelApplication_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            //int ApplicationID = clsLocalDrivingLicenseApplication.Find(ID).ApplicationID;

            clsApplication Application = clsLocalDrivingLicenseApplication.Find(ID).Application;

            if (Application.ApplicationStatus == 2)
            {
                MessageBox.Show("Already Cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (Application.ApplicationStatus == 3)
            {
                MessageBox.Show("He Passed All The Tests.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
               
            var Result = MessageBox.Show("Are you sure you want to cancel this application?","Confirm", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
             
               Application.ApplicationStatus = 2;

               if (Application.Save())
                   MessageBox.Show("Application Cancelled Successfully", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Question);
               else
                   MessageBox.Show("Application Cancel failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         
            }


            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void tsmiScheduleVisionTest_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            frmVisionTestAppointment frmVisionTestAppointment = new frmVisionTestAppointment(ID);
            frmVisionTestAppointment.ShowDialog();

            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;
            
            int PassedTests = clsLocalDrivingLicenseApplication.GetPassedTest(ID);
            string Status = clsLocalDrivingLicenseApplication.GetStatus(ID);

            if (Status == "Cancelled")
            {
                EnabledContextMenuStripItemForStatusCompleted(true);

                tsmScheduleTests.Enabled = false;
                tsmShowLicense.Enabled = false;

                return;

            }

            if (Status == "Completed")
            {
                EnabledContextMenuStripItemForStatusCompleted(false);

                tsmIssueDrivingLicense_FirstTime.Enabled = false;
                tsmShowLicense.Enabled = true;
                return;
            }

            //Status == "New"


            EnabledContextMenuStripItemForStatusCompleted(true);

            tsmShowLicense.Enabled = false;

            EnabledScheduleTestsItem(PassedTests);

            Enabled_IssueDrivingLicense_FirstTime(PassedTests);

        }


        private void EnabledContextMenuStripItemForStatusCompleted(bool Enabled)
        {
            tsmEdit.Enabled = Enabled;
            tsmDelete.Enabled = Enabled;
            tsmCancelApplication.Enabled = Enabled;
            tsmScheduleTests.Enabled = Enabled;
            

        }

        private void EnabledScheduleTestsItem(int PassedTests)
        {
            if (PassedTests == 0)
            {

                tsmiScheduleVisionTest.Enabled = true;
                tsmiScheduleWrittenTest.Enabled = false;
                tsmiScheduleStreetTest.Enabled = false;

            }
            else if (PassedTests == 1)
            {

                tsmiScheduleVisionTest.Enabled = false;
                tsmiScheduleWrittenTest.Enabled = true;
                tsmiScheduleStreetTest.Enabled = false;
            }
            else if (PassedTests == 2)
            {

                tsmiScheduleVisionTest.Enabled = false;
                tsmiScheduleWrittenTest.Enabled = false;
                tsmiScheduleStreetTest.Enabled = true;

            }
            else
            {
                tsmScheduleTests.Enabled = false ;
               

            }

        }

        private void Enabled_IssueDrivingLicense_FirstTime(int PassedTests)
        {
            //Check if the driving license has been issued
            if (PassedTests == 3)
            {
                tsmIssueDrivingLicense_FirstTime.Enabled = true;


            }



        }

        private void tsmScheduleTests_Click(object sender, EventArgs e)
        {

        }

        private void tsmiScheduleWrittenTest_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;
            frmWrittenTestAppointments WrittenTestAppointments = new frmWrittenTestAppointments(ID);

            WrittenTestAppointments.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void tsmiScheduleStreetTest_Click(object sender, EventArgs e)
        {

            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;
            frmStreetTestAppointments StreetTestAppointments = new frmStreetTestAppointments(ID);

            StreetTestAppointments.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();

        }

        private void tsmIssueDrivingLicense_FirstTime_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;
            frmIssueDrivingLicense_FirstTime issueDrivingLicense_FirstTime = new frmIssueDrivingLicense_FirstTime(ID);

            issueDrivingLicense_FirstTime.ShowDialog();


            _RefreshLocalDrivingLicenseApplicationsList();

        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;
            frmLicenseInfo LicenseInfo = new frmLicenseInfo(ID);
            LicenseInfo.ShowDialog();

            //_RefreshLocalDrivingLicenseApplicationsList();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            int PersonID = clsLocalDrivingLicenseApplication.Find(ID).Application.ApplicantPersonID;

            frmLicenseHistory LicenseHistory = new frmLicenseHistory(PersonID);
            LicenseHistory.ShowDialog();

        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            if (clsLocalDrivingLicenseApplication.GetPassedTest(ID) != 0)
            {
                MessageBox.Show("You Cannot Delete This Application Because It Has Process.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete \nthe Application by Local Driving License Application ID: {ID}", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                if (clsLocalDrivingLicenseApplication.DeleteLocalDLApplications(ID))
                {
                   
                    MessageBox.Show("Application has been deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Application was not deleted because it has data linked to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }



            }

            _RefreshLocalDrivingLicenseApplicationsList();


        }

        private void tsmShowApplicationDetails_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllLocalDLApplications.CurrentRow.Cells[0].Value;

            frmApplicationDetails frmApplicationDetails = new frmApplicationDetails(ID);
            frmApplicationDetails.ShowDialog();



        }


    }
}
