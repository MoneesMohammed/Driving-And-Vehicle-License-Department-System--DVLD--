using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications;
using Driving___Vehicle_License_Department__DVLD_.Properties;
using DVLD_BusinessLayer;
using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_DataAccessLayar.clsTestType;

namespace Driving___Vehicle_License_Department__DVLD_.Tests
{
    public partial class frmListTestAppointment : Form
    {

        private DataTable _dtAppointmentTest;
        private int _LocalDrivingLicenseApplicationID = -1;
        private enTestType _TestTypeID = enTestType.VisionTest;



        public frmListTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;

        }

        private void frmListTestAppointment_Load(object sender, EventArgs e)
        {
            ucDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDLAppID(_LocalDrivingLicenseApplicationID);
            _RefreshAppointmentTestList();
        }

        
        private void _RefreshAppointmentTestList()
        {
            _dtAppointmentTest = clsTestAppointment.GetAllTestAppointments(_LocalDrivingLicenseApplicationID, _TestTypeID);

            dgvAllAppointmentTest.DataSource = _dtAppointmentTest;

            lblRecodes.Text = dgvAllAppointmentTest.Rows.Count.ToString();

            if (_dtAppointmentTest.Rows.Count > 0)
            {
                dgvAllAppointmentTest.Columns[0].HeaderText = "Appointment ID";
                dgvAllAppointmentTest.Columns[0].Width = 150;

                dgvAllAppointmentTest.Columns[1].HeaderText = "Appointment Date";
                dgvAllAppointmentTest.Columns[1].Width = 200;

                dgvAllAppointmentTest.Columns[2].HeaderText = "Paid Fees";
                dgvAllAppointmentTest.Columns[2].Width = 150;

                dgvAllAppointmentTest.Columns[3].HeaderText = "Is Locked";
                dgvAllAppointmentTest.Columns[3].Width = 100;

            }


            switch (_TestTypeID)
            {
                case enTestType.VisionTest:
                    pictureBox1.Image = Resources.iris_recognition;
                    lblTitle.Text = "Vision Test Appointment";
                    this.Text = "Vision Test Appointment";

                    lblTitle.ForeColor = System.Drawing.Color.DodgerBlue;
                    
                    pictureBox1.Size = new System.Drawing.Size(242, 154);
                    pictureBox1.Location = new System.Drawing.Point(401, 12);
                    break;
                case enTestType.WrittenTest:
                    pictureBox1.Image = Resources.exam_1;
                    lblTitle.Text = "Written Test Appointment";
                    this.Text = "Written Test Appointment";
                    lblTitle.ForeColor = System.Drawing.Color.Red;
                    
                    
                    pictureBox1.Size = new System.Drawing.Size(166, 154);
                    pictureBox1.Location = new System.Drawing.Point(438, 12);
                    break;
                case enTestType.StreetTest:
                    pictureBox1.Image = Resources.driving_test_2;
                    lblTitle.Text = "Street Test Appointment";
                    this.Text = "Street Test Appointment";

                    lblTitle.ForeColor = System.Drawing.Color.DarkOrange;
                    

                    pictureBox1.Size = new System.Drawing.Size(160, 154);
                    pictureBox1.Location = new System.Drawing.Point(444, 12);

                    break;
                default:
                    break;
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(_LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add a new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
            frmScheduleTest.ShowDialog();

            _RefreshAppointmentTestList();
        }

        /* private bool CheckForAppointmentsAndPassTest()
         {
             bool AllIsLocked = clsTestAppointment.AllIsLockedByTestTypeIDAndLDLAppID((int)clsTestType.enTestType.VisionTest, _LDLAppID);
             bool IsTestAppointmentExists = clsTestAppointment.IsTestAppointmentExists((int)clsTestType.enTestType.VisionTest, _LDLAppID);
             bool IsPassed    = clsTest.CheckPassedTest((int)clsTestType.enTestType.VisionTest, _LDLAppID);

             if (IsTestAppointmentExists)
             {
                 if (!AllIsLocked)
                 {
                     MessageBox.Show("Person Already have an active appointment for this test, You cannot add a new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return true;
                 }
                 else if (IsPassed)
                 {
                     MessageBox.Show("This person already passed this test before. You can only retake the failed test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return true;
                 }

             }



             return false;
         }
        */


        private void tsmEdit_Click(object sender, EventArgs e)
        {

            int AppointmentTestID = (int)dgvAllAppointmentTest.CurrentRow.Cells[0].Value;

            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID, AppointmentTestID);
            frmScheduleTest.ShowDialog();

            _RefreshAppointmentTestList();
        }

        private void tsmTakeTest_Click(object sender, EventArgs e)
        {
            int AppointmentTestID = (int)dgvAllAppointmentTest.CurrentRow.Cells[0].Value;

            frmTakeTest frmTakeTest = new frmTakeTest(AppointmentTestID);
            frmTakeTest.ShowDialog();

            _RefreshAppointmentTestList();
        }

       
    }
}
