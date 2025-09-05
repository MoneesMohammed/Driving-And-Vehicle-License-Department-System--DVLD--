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
    public partial class frmIssueDrivingLicense_FirstTime : Form
    {
        private int _LDLAppID;

        public clsLocalDrivingLicenseApplication LDLApplication = new clsLocalDrivingLicenseApplication();

        public clsLicenses License = new clsLicenses();

        public frmIssueDrivingLicense_FirstTime(int LDLAppID)
        {
            InitializeComponent();

            _LDLAppID = LDLAppID;

            LDLApplication = clsLocalDrivingLicenseApplication.Find(LDLAppID);

        }

        private void frmIssueDrivingLicense_FirstTime_Load(object sender, EventArgs e)
        {

            _LoadData();


        }

        private void _LoadData()
        {
            ucDrivingLicenseApplicationInfo1.RefreshUcDrivingLicenseApplicationInfo(_LDLAppID);

            int ApplicationID = LDLApplication.ApplicationID ;

            //textBox1.Text = "LDLAppID: " + _LDLAppID.ToString() + "  ApplicationID: " + ApplicationID.ToString();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            License.ApplicationID   = LDLApplication.ApplicationID;
            License.LicenseClass    = LDLApplication.LicenseClassID;
            License.CreatedByUserID = frmLogin.user.UserID;

            if (License.Save())
            {
                LDLApplication.Application.ApplicationStatus = 3;
                LDLApplication.Application.LastStatusDate = DateTime.Now;

                if(LDLApplication.Application.Save())
                    MessageBox.Show($"License Issued Successfully with License ID = {License.LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                   MessageBox.Show("Error : data is not saved successfully", "Application", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssue.Enabled = false;
            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully", "License", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
    }
}
