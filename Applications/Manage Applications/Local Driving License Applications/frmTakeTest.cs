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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications
{
    public partial class frmTakeTest : Form
    {
        private int _LDLAppID;

        public clsTest Test = new clsTest();

        //public clsTestType TestType = clsTestType.Find(1);// 1 he is Vision Test
        public clsTestType TestType = new clsTestType();
        
        public clsLocalDrivingLicenseApplication LocalLDApplication = new clsLocalDrivingLicenseApplication();
        public clsApplication Application = new clsApplication();
        public clsTestAppointment TestAppointments = new clsTestAppointment();

        

        public frmTakeTest(int lDLAppID , int TestAppointmentID , int TeatTypeID)
        {
            InitializeComponent();
            _LDLAppID = lDLAppID;
            TestType = clsTestType.Find(TeatTypeID);

            TestAppointments = clsTestAppointment.Find(TestAppointmentID);

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {
            LocalLDApplication = clsLocalDrivingLicenseApplication.Find(_LDLAppID);

            if (LocalLDApplication == null)
            {
                //DefaultInfo();
                return;
            }

            Application = clsApplication.Find(LocalLDApplication.ApplicationID);
            
            lblDLAppID.Text = _LDLAppID.ToString();
            lblDClass.Text  = LocalLDApplication.LicenseClass.ClassName;

            lblName.Text = (Application.Person.ThirdName == "") ?
                $"{Application.Person.FirstName} {Application.Person.SecondName} {Application.Person.LastName}" :
                $"{Application.Person.FirstName} {Application.Person.SecondName} {Application.Person.ThirdName} {Application.Person.LastName}";

            lblTrial.Text = clsTestAppointment.CountRetakeTest(TestType.TestTypeID, _LDLAppID).ToString();
            lblFees.Text = ((int)TestType.TestTypeFees).ToString();

            

            lblDate.Text = TestAppointments.AppointmentDate.ToString("dd/mm/yyyy");

            if (TestType.TestTypeID == 2)
            {
                pictureBox1.Image = Resources.exam_1;

            }
            else if (TestType.TestTypeID == 3)
            {
                pictureBox1.Image = Resources.driving_test_2;
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            var Result = MessageBox.Show("Are you sure you want to save? After that, you cannot change the Pass/Fail results after you save?", "Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
          
            if (Result == DialogResult.No)
                return;
            
            Test.TestAppointmentID = TestAppointments.TestAppointmentID;
            Test.TestResult = rbSuccess.Checked;
            Test.Notes = txtNote.Text;
            Test.CreatedByUserID = frmLogin.user.UserID;

            TestAppointments.IsLocked = true;

            

            if (Test.Save())
            {
                if (TestAppointments.Save())
                   MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                  MessageBox.Show("Error : data is not saved successfully for TestAppointments", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully for Test", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            lblTestID.Text = Test.TestID.ToString();


        }


    }
}
