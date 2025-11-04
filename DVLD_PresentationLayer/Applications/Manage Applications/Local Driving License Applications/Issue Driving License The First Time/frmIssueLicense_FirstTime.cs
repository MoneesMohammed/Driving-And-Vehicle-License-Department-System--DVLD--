using DVLD.Classes;
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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Issue_Driving_License_The_First_Time
{
    public partial class frmIssueLicense_FirstTime : Form
    {

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        

        public frmIssueLicense_FirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }


        private void frmIssueDrivingLicense_FirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show($"No Application with ID = {_LocalDrivingLicenseApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;

            }

            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show("Person Should Pass All Tests First", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;

            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            if (LicenseID != -1)
            {

                MessageBox.Show($"Person already has license before with ID = {LicenseID} ", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }


           ucDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDLAppID(_LocalDrivingLicenseApplicationID);

        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFirtTime(txtNotes.Text, clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                
              MessageBox.Show($"License Issued Successfully with License ID = {LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Close();
                   
            }
            else
            {

                MessageBox.Show("License Was not Issued !", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
    }
}
