using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
using DVLD.Classes;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.UserControls
{
    public partial class ucDrivingLicenseApplicationInfo : UserControl
    {
        private int _LocalDLApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDLApplication = new clsLocalDrivingLicenseApplication();

        private int _LicenseID;

        public int LocalLDApplicationID
        { 
           get { return _LocalDLApplicationID; }
        }

        public clsLocalDrivingLicenseApplication SelectedLocalDLApplicationInfo
        {
           get { return _LocalDLApplication; }
        }

        public ucDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfoByLocalDLAppID(int LocalDLAppID)
        {
            _LocalDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(LocalDLAppID);

            if (_LocalDLApplication == null)
            {
                ResetDefaultApplicationInfo();
                MessageBox.Show($"No Application With [ Local_Driving_License_ApplicationID ] ={LocalDLAppID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillApplicationInfo();
        }


        public void LoadApplicationInfoApplicationID(int ApplicationID)
        {
            _LocalDLApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            if (_LocalDLApplication == null)
            {
                ResetDefaultApplicationInfo();
                MessageBox.Show($"No Application With [ ApplicationID ] ={ApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillApplicationInfo();
        }


        private void _FillApplicationInfo()
        {
            _LicenseID = _LocalDLApplication.GetActiveLicenseID();

            int PassedTests = _LocalDLApplication.GetPassedTestCount();

            lblDLAppID.Text = _LocalDLApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedForLicense.Text = _LocalDLApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = $"{PassedTests}/3";

            lblID.Text = _LocalDLApplication.ApplicationID.ToString();
            lblStatus.Text = _LocalDLApplication.StatusText;

            lblType.Text = _LocalDLApplication.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicant.Text = _LocalDLApplication.PersonFullName;
            lblFees.Text = _LocalDLApplication.ApplicationTypeInfo.ApplicationFees.ToString("00");
            lblDate.Text = _LocalDLApplication.ApplicationDate.ToString("dd/MMM/yyyy");
            lblStatusDate.Text = _LocalDLApplication.LastStatusDate.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;


            llblShowLicenseInfo.Enabled = (PassedTests == 3) && (lblStatus.Text == "Completed");

            llblShowLicenseInfo.Enabled = (_LicenseID != -1);


        }



        private void ResetDefaultApplicationInfo()
        {
            lblDLAppID.Text           = "[????]";
            lblAppliedForLicense.Text = "[????]";
            lblPassedTests.Text       = "[????]";
                                      
            lblID.Text                = "[????]";
            lblStatus.Text            = "[????]";
            lblType.Text              = "[????]";
            lblApplicant.Text         = "[????]";
            lblFees.Text              = "[$$$$]";
                                       
            lblDate.Text              = "[????/??/??]";
            lblStatusDate.Text        = "[????]";
            lblCreatedBy.Text         = "[????]";


        }



        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDetailsPerson frmShowDetailsPerson = new frmShowDetailsPerson(_LocalDLApplication.ApplicantPersonID);
            frmShowDetailsPerson.ShowDialog();

            LoadApplicationInfoByLocalDLAppID(_LocalDLApplication.LocalDrivingLicenseApplicationID);
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            int ApplicationID = _LocalDLApplication.ApplicationID;

            int LicenseID = clsLicense.FindByApplicationID(ApplicationID).LicenseID;

            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(LicenseID);
            frmLicenseInfo.ShowDialog();
        }

    }
}
