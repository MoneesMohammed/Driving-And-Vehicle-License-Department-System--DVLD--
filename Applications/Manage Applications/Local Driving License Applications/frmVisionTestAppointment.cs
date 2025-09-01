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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications
{
    public partial class frmVisionTestAppointment : Form
    {
        private int _LDLAppID;
        public clsTestType TestType = clsTestType.Find(1);

        private DataTable AppointmentTestDataTable = new DataTable();

        public frmVisionTestAppointment(int LDLAppID)
        {
            InitializeComponent();
            _LDLAppID = LDLAppID;

        }

        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            ucDrivingLicenseApplicationInfo1.RefreshUcDrivingLicenseApplicationInfo(_LDLAppID);
            _RefreshAppointmentTestList();
        }

        private void _RefreshAppointmentTestList()
        {
            AppointmentTestDataTable = clsTestAppointment.GetAllTestAppointments(TestType.TestTypeID, _LDLAppID);

            dgvAllAppointmentTest.DataSource = AppointmentTestDataTable;

            lblRecodes.Text = AppointmentTestDataTable.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (CheckForAppointmentsAndPassTest())
            {
                return;
            }


            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LDLAppID, TestType.TestTypeID);
            frmScheduleTest.ShowDialog();
            _RefreshAppointmentTestList();
        }

        private bool CheckForAppointmentsAndPassTest()
        {
            bool AllIsLocked = clsTestAppointment.AllIsLockedByTestTypeIDAndLDLAppID(TestType.TestTypeID, _LDLAppID);
            bool IsTestAppointmentExists = clsTestAppointment.IsTestAppointmentExists(TestType.TestTypeID, _LDLAppID);
            bool IsPassed    = clsTest.CheckPassedTest(TestType.TestTypeID, _LDLAppID);

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



        private void tsmEdit_Click(object sender, EventArgs e)
        {

            int ID = (int)dgvAllAppointmentTest.CurrentRow.Cells[0].Value;

            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LDLAppID, ID, TestType.TestTypeID);
            frmScheduleTest.ShowDialog();
            _RefreshAppointmentTestList();
        }

        private void tsmTakeTest_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllAppointmentTest.CurrentRow.Cells[0].Value;

            frmTakeTest frmTakeTest = new frmTakeTest(_LDLAppID, ID, TestType.TestTypeID);
            frmTakeTest.ShowDialog();

            _RefreshAppointmentTestList();
        }
    }
}
