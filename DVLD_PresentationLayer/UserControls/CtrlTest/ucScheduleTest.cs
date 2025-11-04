using Driving___Vehicle_License_Department__DVLD_.Properties;
using DVLD.Classes;
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
using static Driving___Vehicle_License_Department__DVLD_.UserControls.CtrlTest.ucScheduleTest;
using static DVLD_DataAccessLayar.clsTestType;

namespace Driving___Vehicle_License_Department__DVLD_.UserControls.CtrlTest
{
    public partial class ucScheduleTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 }
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;



        private clsLocalDrivingLicenseApplication _LocalLDApplication;
        private int _LDLAppID = -1;

        private clsTestType _TestType;
        private enTestType _TestTypeID = enTestType.VisionTest;

        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID =-1;

        public enTestType TestTypeID
        {
            get { return _TestTypeID; }

            set 
            {
                 _TestTypeID = value;

                switch (TestTypeID)
                {
                    case enTestType.VisionTest:
                        pictureBox1.Image = Resources.iris_recognition;
                        gbTestType.Text   = "Vision Test";
                        pictureBox1.Size = new System.Drawing.Size(181, 159);
                        pictureBox1.Location = new System.Drawing.Point(188, 25);
                        break;
                    case enTestType.WrittenTest:
                        pictureBox1.Image = Resources.exam_1;
                        gbTestType.Text   = "Written Test";
                        pictureBox1.Size = new System.Drawing.Size(181, 159);
                        pictureBox1.Location = new System.Drawing.Point(215, 25);
                        break;
                    case enTestType.StreetTest:
                        pictureBox1.Image = Resources.driving_test_2;
                        gbTestType.Text   = "Street Test";
                        pictureBox1.Size = new System.Drawing.Size(181, 159);
                        pictureBox1.Location = new System.Drawing.Point(188, 25);
                        break;
                    default:
                        break;
                }

            }
        }



        public ucScheduleTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int lDLAppID,int TestAppointmentID = -1)
        {
            _Mode = (TestAppointmentID == -1) ? enMode.AddNew : enMode.Update;

            _LDLAppID = lDLAppID;
            _TestAppointmentID = TestAppointmentID;

            _LocalLDApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(lDLAppID);
            
            if (_LocalLDApplication == null)
            {
                ResetScheduleTestInfo();
                MessageBox.Show($"Error : No Local Driving License Application With ID ={lDLAppID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LocalLDApplication.DoesAttendTestType(TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;


            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {

                gbRetakeTestInfo.Enabled = true;

                lblTital.Text = "Schedule Retake Test";
                lblRTestAppID.Text = "0";
                lblRAppFees.Text = (clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees).ToString("00");

            }
            else
            {

                gbRetakeTestInfo.Enabled = false;

                lblTital.Text = "Schedule Test";
                lblRTestAppID.Text = "N/A";
                lblRAppFees.Text = "0";
            }




            _FillScheduleTestInfo();
        }


       
        private void _FillScheduleTestInfo()
        {
            

            lblDLAppID.Text = _LocalLDApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _LocalLDApplication.LicenseClassInfo.ClassName;
            lblName.Text = _LocalLDApplication.PersonFullName;

            lblTrial.Text = _LocalLDApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(_TestTypeID).TestTypeFees.ToString();
                dtpDateTest.MinDate = DateTime.Now;
                lblRTestAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }
            
           lblTotalFees.Text = (Convert.ToSingle (lblFees.Text) + Convert.ToSingle(lblRAppFees.Text)).ToString();


            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePrviousTestConstraint())
                return;

        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LDLAppID, this.TestTypeID))
            {
                lblUesrMessage.Visible = true;
                lblUesrMessage.Text = "Person already sat for the test, appointment locked.";

                btnSave.Enabled = false;
                dtpDateTest.Enabled = false;

                return false;
            }
            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUesrMessage.Visible = true;
                lblUesrMessage.Text = "Person already sat for the test, appointment locked.";

                btnSave.Enabled = false;
                dtpDateTest.Enabled = false;

                return false;
            }
            else
                lblUesrMessage.Visible = false;


            return true;

        }

        private bool _HandlePrviousTestConstraint()
        {

            switch (_TestTypeID)
            {
                case enTestType.VisionTest:
                    lblUesrMessage.Visible = false;
                    return true;
                    
                case enTestType.WrittenTest:

                    if (!_LocalLDApplication.DoesPassTestType(enTestType.VisionTest))
                    {
                        lblUesrMessage.Visible = true;
                        lblUesrMessage.Text = "Cannot Schedule , Vision Test Should Be Passed First.";

                        btnSave.Enabled = false;
                        dtpDateTest.Enabled = false;

                        return false;
                    }
                    else
                    {
                        lblUesrMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpDateTest.Enabled = true;

                        return true;
                    }


                case enTestType.StreetTest:

                    if (!_LocalLDApplication.DoesPassTestType(enTestType.WrittenTest))
                    {
                        lblUesrMessage.Visible = true;
                        lblUesrMessage.Text = "Cannot Schedule , Written Test Should Be Passed First.";

                        btnSave.Enabled = false;
                        dtpDateTest.Enabled = false;

                        return false;
                    }
                    else
                    {
                        lblUesrMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpDateTest.Enabled = true;

                        return true;
                    }
                    
                default:
                    break;
            }

            return true;
        }

        private bool _HandleRetakeApplication()
        {

            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            { 
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = _LocalLDApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;


                if (!Application.Save())
                { 
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show($"Faild Create Application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
            }

            return true;
        
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);


            if (_LocalLDApplication == null)
            {
                ResetScheduleTestInfo();
                MessageBox.Show($"Error : No Test Appointment With ID = {_TestAppointmentID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpDateTest.Value = DateTime.Now;
            else
                dtpDateTest.MinDate = _TestAppointment.AppointmentDate;

            dtpDateTest.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRTestAppID.Text = "N/A";
                lblRAppFees.Text = "0";
            }
            else
            {

                lblRAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                lblTital.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
                lblRTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }


            return true;
        }


        private void ResetScheduleTestInfo()
        {
            _LDLAppID = -1;
            
            lblDLAppID.Text   = "[???]";
            lblDClass.Text    = "[???]";
            lblName.Text      = "[???]";
            lblTrial.Text     = "[???]";
            dtpDateTest.Value = DateTime.Now;
            lblFees.Text      = "[???]";

            gbRetakeTestInfo.Enabled = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalLDApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.PaidFees = Convert.ToDecimal(lblFees.Text);
            _TestAppointment.AppointmentDate = dtpDateTest.Value;
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }
    }
}
