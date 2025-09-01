using Driving___Vehicle_License_Department__DVLD_.Properties;
using DVLD_BusinessLayer;
using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications
{
    public partial class frmScheduleTest : Form
    {
        private int _LDLAppID { get; set; }

        public enum enMode { AddNew = 0, Update = 1 , AddRetakeTest = 2 }
        private enMode _Mode = enMode.AddNew;

       

        public clsTestAppointment TestAppointments = new clsTestAppointment();

        //public clsTestType TestType = clsTestType.Find(1);
        public clsTestType TestType = new clsTestType();

        public clsApplication Application = new clsApplication();

        public clsApplication RetakeTestApplication = new clsApplication(); //Retake Test Application

        public clsApplicationType ApplicationType = clsApplicationType.Find(7);//Retake Test

        public clsLocalDrivingLicenseApplication LocalLDApplication = new clsLocalDrivingLicenseApplication();

        public bool AllIsLocked ;
        public bool IsTestAppointmentExists ;
        public bool IsPassed;


        public frmScheduleTest(int lDLAppID ,int TeatTypeID)
        {
            InitializeComponent();
            TestType = clsTestType.Find(TeatTypeID);

            _LDLAppID = lDLAppID;

            AllIsLocked = clsTestAppointment.AllIsLockedByTestTypeIDAndLDLAppID(TestType.TestTypeID, _LDLAppID);
            IsTestAppointmentExists = clsTestAppointment.IsTestAppointmentExists(TestType.TestTypeID, _LDLAppID);
            IsPassed = clsTest.CheckPassedTest(TestType.TestTypeID, _LDLAppID);


            if (!IsTestAppointmentExists )
                _Mode = enMode.AddNew;
            else if(!IsPassed )
                _Mode = enMode.AddRetakeTest;
            
        }

       



        public frmScheduleTest(int lDLAppID, int TestAppointmentID , int TeatTypeID)
        {
            // For Update

            InitializeComponent();
            TestType = clsTestType.Find(TeatTypeID);

            _LDLAppID = lDLAppID;

            AllIsLocked = clsTestAppointment.AllIsLockedByTestTypeIDAndLDLAppID(TestType.TestTypeID, _LDLAppID);
            IsTestAppointmentExists = clsTestAppointment.IsTestAppointmentExists(TestType.TestTypeID, _LDLAppID);
            IsPassed = clsTest.CheckPassedTest(TestType.TestTypeID, _LDLAppID);

            TestAppointments = clsTestAppointment.Find(TestAppointmentID);

            _Mode = enMode.Update;

        }


        private void frmScheduleTest_Load(object sender, EventArgs e)
        {

            _LoadData();

            if (_Mode == enMode.Update)
            {
                if (TestAppointments.IsLocked)
                {
                    btnSave.Enabled = false;
                    dtpDateTest.Enabled = false;

                    gbRetakeTestInfo.Enabled = true;
                    lblMessIsLocked.Visible = true;

                }
                
            }

        }

        private void _LoadData()
        {
            if (_Mode == enMode.AddRetakeTest)
                lblMode.Text = "Retake Test Mode";
            else if(_Mode == enMode.AddNew)
                lblMode.Text = "Add Mode";
            else if(_Mode == enMode.Update)
                lblMode.Text = "Update";

            LocalLDApplication = clsLocalDrivingLicenseApplication.Find(_LDLAppID);
            Application = LocalLDApplication.Application;


            lblDClass.Text = LocalLDApplication.LicenseClass.ClassName;

            lblDLAppID.Text = _LDLAppID.ToString();
            

            lblName.Text = (Application.Person.ThirdName == "") ?
                $"{Application.Person.FirstName} {Application.Person.SecondName} {Application.Person.LastName}" : 
                $"{Application.Person.FirstName} {Application.Person.SecondName} {Application.Person.ThirdName} {Application.Person.LastName}" ;

            lblTrial.Text = clsTestAppointment.CountRetakeTest(TestType.TestTypeID, _LDLAppID).ToString() ;

            lblFees.Text = ((int)TestType.TestTypeFees).ToString();

            dtpDateTest.Value = DateTime.Now;

            if (TestType.TestTypeID == 2)
            {
                pictureBox1.Image = Resources.exam_1;

            }
            else if (TestType.TestTypeID == 3)
            {
                pictureBox1.Image = Resources.driving_test_2;
            }


            if (_Mode == enMode.AddNew)
                return;

           // dtpDateTest.Value = TestAppointments.AppointmentDate;


            if (_Mode == enMode.Update)
                return;

            



            gbRetakeTestInfo.Enabled = true;

            lblRAppFees.Text = ((int)ApplicationType.ApplicationFees).ToString(); 

            lblTotalFees.Text = (((int) TestType.TestTypeFees) + int.Parse(lblRAppFees.Text)).ToString();



        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                TestAppointments.AppointmentDate = dtpDateTest.Value;
                TestAppointments.CreatedByUserID = frmLogin.user.UserID;
            }
            else if (_Mode == enMode.AddNew)
            {
                TestAppointments.TestTypeID = TestType.TestTypeID;
                TestAppointments.LocalDrivingLicenseApplicationID = _LDLAppID;
                TestAppointments.PaidFees = TestType.TestTypeFees;
                TestAppointments.AppointmentDate = dtpDateTest.Value;
                TestAppointments.CreatedByUserID = frmLogin.user.UserID;
                TestAppointments.IsLocked = false;

            }
            else if (_Mode == enMode.AddRetakeTest)
            {
                RetakeTestApplication.ApplicantPersonID = Application.ApplicantPersonID;
                RetakeTestApplication.ApplicationDate   = DateTime.Now;
                RetakeTestApplication.ApplicationTypeID = ApplicationType.ApplicationTypeID;
                RetakeTestApplication.ApplicationStatus = 3;
                RetakeTestApplication.LastStatusDate    = DateTime.Now;
                RetakeTestApplication.PaidFees          = ApplicationType.ApplicationFees;
                RetakeTestApplication.CreatedByUserID   = frmLogin.user.UserID;

                if (RetakeTestApplication.Save())
                {
                    TestAppointments.TestTypeID                       = TestType.TestTypeID;
                    TestAppointments.LocalDrivingLicenseApplicationID = _LDLAppID;
                    TestAppointments.PaidFees                         = TestType.TestTypeFees;
                    TestAppointments.AppointmentDate                  = dtpDateTest.Value;
                    TestAppointments.RetakeTestApplicationID          = RetakeTestApplication.ApplicationID;
                    TestAppointments.CreatedByUserID                  = frmLogin.user.UserID;
                    TestAppointments.IsLocked                         = false;

                }
                else
                {

                    MessageBox.Show("Error : data is not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblRTestAppID.Text = RetakeTestApplication.ApplicationID.ToString();

            }



            if (TestAppointments.Save())
            {
                
                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            

        }
    }
}
