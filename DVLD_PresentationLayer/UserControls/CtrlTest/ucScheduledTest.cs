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

namespace Driving___Vehicle_License_Department__DVLD_.UserControls.CtrlTest
{
    public partial class ucScheduledTest : UserControl
    {

        private clsTestType.enTestType _TestTypeID = enTestType.VisionTest;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID;
        private int _TestAppointmentID = -1;
        clsTestAppointment _TestAppointment;
        private int _TestID = -1;


        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestID;
            }
        }


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
                        gbTestType.Text = "Vision Test";
                        pictureBox1.Size = new System.Drawing.Size(181, 159);
                        pictureBox1.Location = new System.Drawing.Point(188, 25);
                        break;
                    case enTestType.WrittenTest:
                        pictureBox1.Image = Resources.exam_1;
                        gbTestType.Text = "Written Test";
                        pictureBox1.Size = new System.Drawing.Size(181, 159);
                        pictureBox1.Location = new System.Drawing.Point(215, 25);
                        break;
                    case enTestType.StreetTest:
                        pictureBox1.Image = Resources.driving_test_2;
                        gbTestType.Text = "Street Test";
                        pictureBox1.Size = new System.Drawing.Size(181, 159);
                        pictureBox1.Location = new System.Drawing.Point(188, 25);
                        break;
                    default:
                        break;
                }

            }
        }

        public ucScheduledTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (_TestAppointment == null)
            {
                ResetScheduledTestInfo();
                MessageBox.Show($"Error : No Test Appointment With ID ={TestAppointmentID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.TestTypeID    = _TestAppointment.TestTypeID;
            this._TestAppointmentID = TestAppointmentID;

            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(_LocalDrivingLicenseApplicationID);

            lblDLAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text  = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblName.Text    = _LocalDrivingLicenseApplication.PersonFullName;

            lblTrial.Text   = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
            lblFees.Text    = (clsTestType.Find(_TestTypeID).TestTypeFees).ToString();

            lblDate.Text    = _TestAppointment.AppointmentDate.ToString("dd/MM/yyyy");


            _TestID = _TestAppointment.TestID;

            if (_TestID != -1)
                lblTestID.Text = _TestID.ToString();
        }

        public void LoadTestID(int TestID)
        {
            lblTestID.Text = TestID.ToString();
        }



        private void ResetScheduledTestInfo()
        {
            _TestAppointmentID = -1;

            lblDLAppID.Text = "[???]";
            lblDClass.Text  = "[???]";
            lblName.Text    = "[???]";

            lblTrial.Text   = "[???]";
            lblFees.Text    = "[$$$]";
            lblTestID.Text  = "Not Taken Yet";
            lblDate.Text    = "[??/??/????]";

        }




    }
}
