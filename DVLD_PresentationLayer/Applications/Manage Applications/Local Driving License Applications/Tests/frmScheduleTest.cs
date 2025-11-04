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
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications
{
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private int _TestAppointmentID = -1;


        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TeatTypeID , int TestAppointmentID = -1)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TeatTypeID;
            _TestAppointmentID = TestAppointmentID;
           
        }


        private void frmScheduleTest_Load(object sender, EventArgs e)
        {

           ucScheduleTest1.TestTypeID = _TestTypeID;
           ucScheduleTest1.LoadInfo(_LocalDrivingLicenseApplicationID, _TestAppointmentID);

        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
