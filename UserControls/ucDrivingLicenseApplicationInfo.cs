using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
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
        public clsLocalDrivingLicenseApplication LocalLDApplication = new clsLocalDrivingLicenseApplication();
        public clsPerson Person = new clsPerson();
        public clsApplicationType ApplicationType = new clsApplicationType();
        public clsApplication Application = new clsApplication();



        public ucDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void RefreshUcDrivingLicenseApplicationInfo(int LDLAppID)
        {
            LocalLDApplication = clsLocalDrivingLicenseApplication.Find(LDLAppID);

            if (LocalLDApplication == null)
            {
                DefaultUcDrivingLicenseApplicationInfo();
                return;
            }
                

            Application = LocalLDApplication.Application;

            ApplicationType = Application.ApplicationType;

            Person = Application.Person;


            string LicenseClass = clsLicenseClass.Find(LocalLDApplication.LicenseClassID).ClassName;

            int PassedTests = clsLocalDrivingLicenseApplication.GetPassedTest(LDLAppID);

            int Status = Application.ApplicationStatus;

            lblDLAppID.Text           = LDLAppID.ToString();
            lblAppliedForLicense.Text = LicenseClass;
            lblPassedTests.Text       = $"{PassedTests}/3";

            lblID.Text                = LocalLDApplication.ApplicationID.ToString();
            lblStatus.Text            = (Status == 1) ? "New": (Status == 2) ? "Cancelled" : "Completed";
            lblType.Text              = ApplicationType.ApplicationTypeTitle;

            lblApplicant.Text  = (Person.ThirdName == "") ?
                $"{Person.FirstName} {Person.SecondName} {Person.LastName}" :
                $"{Person.FirstName} {Person.SecondName} {Person.ThirdName} {Person.LastName}";

            lblFees.Text              = ((int)ApplicationType.ApplicationFees).ToString();

            lblDate.Text              = Application.ApplicationDate.ToString("dd/MMM/yyyy");
            lblStatusDate.Text        = Application.LastStatusDate.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text         = frmLogin.user.UserName;


            llblShowLicenseInfo.Enabled = (PassedTests == 3) && (Status == 3);


        }


        private void DefaultUcDrivingLicenseApplicationInfo()
        {
            lblDLAppID.Text           = "[????]";
            lblAppliedForLicense.Text = "[????]";
            lblPassedTests.Text       = "[????]";
                                      
            lblID.Text                = "[????]";
            lblStatus.Text            = "[????]";
            lblType.Text              = "[????]";
            lblApplicant.Text         = "[????]";
            lblFees.Text              = "[????]";
                                       
            lblDate.Text              = "[????]";
            lblStatusDate.Text        = "[????]";
            lblCreatedBy.Text         = "[????]";


        }


       

        private void ucDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {

            RefreshUcDrivingLicenseApplicationInfo(LocalLDApplication.LocalDrivingLicenseApplicationID);

        }

        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDetailsPerson frmShowDetailsPerson = new frmShowDetailsPerson(Application.ApplicantPersonID);
            frmShowDetailsPerson.ShowDialog();

            RefreshUcDrivingLicenseApplicationInfo(LocalLDApplication.LocalDrivingLicenseApplicationID);
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int ID = LocalLDApplication.LocalDrivingLicenseApplicationID;
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(ID);
            frmLicenseInfo.ShowDialog();
        }

    }
}
