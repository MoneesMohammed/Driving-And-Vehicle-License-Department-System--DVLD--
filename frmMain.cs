using Driving___Vehicle_License_Department__DVLD_.Applications;
using Driving___Vehicle_License_Department__DVLD_.Applications.Detain_Licenses;
using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.International_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.Local_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.Renew_Driving_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.Replacement_for_Lost_or_Damaged_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.International_License_Applications;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Test_Types;
using Driving___Vehicle_License_Department__DVLD_.Manage_Drivers;
using Driving___Vehicle_License_Department__DVLD_.Manage_Users;
using DVLD_BusinessLayer;
using System;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void People_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();

            
        }

        private void signOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //frmLogin frmLogin = new frmLogin();
            //frmLogin.Show();
            

            this.Close();
        }

        private void Users_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers frmManageUsers = new frmManageUsers();
            frmManageUsers.ShowDialog();


        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = frmLogin.user.UserID;

            frmUserInfo frmUserInfo = new frmUserInfo(ID);
            frmUserInfo.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = frmLogin.user.UserID;

            frmChangePassword frmChangePassword = new frmChangePassword(ID);
            frmChangePassword.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frmManageApplicationTypes = new frmManageApplicationTypes();
            frmManageApplicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frmManageTestTypes = new frmManageTestTypes();
            frmManageTestTypes.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicense = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicense.ShowDialog();

        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frmLocalDrivingLicenseApplications = new frmLocalDrivingLicenseApplications();
            frmLocalDrivingLicenseApplications.ShowDialog();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            label1.Text = $"Welcome : {frmLogin.user.UserName}";

        }

        private void Drivers_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frmListDrivers = new frmListDrivers();
            frmListDrivers.ShowDialog();

        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frmNewInternationalLicenseApplication = new frmNewInternationalLicenseApplication();
            frmNewInternationalLicenseApplication.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicensingApplications frmListInternational = new frmListInternationalLicensingApplications();
            frmListInternational.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicense frmRenewLocalDrivingLicense = new frmRenewLocalDrivingLicense();
            frmRenewLocalDrivingLicense.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementForDamagedLicenses forDamagedLicenses = new frmReplacementForDamagedLicenses();
            forDamagedLicenses.ShowDialog();

        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frmLocalDrivingLicenseApplications = new frmLocalDrivingLicenseApplications();
            frmLocalDrivingLicenseApplications.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frmReleaseDetainedLicense = new frmReleaseDetainedLicense();
            frmReleaseDetainedLicense.ShowDialog();

        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frmListDetainedLicenses = new frmListDetainedLicenses();
            frmListDetainedLicenses.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frmReleaseDetainedLicense = new frmReleaseDetainedLicense();
            frmReleaseDetainedLicense.ShowDialog();
        }
    }
}
