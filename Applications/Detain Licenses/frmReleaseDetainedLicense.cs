using DVLD_BusinessLayer;
using System;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Detain_Licenses
{
    public partial class frmReleaseDetainedLicense : Form
    {
        public clsApplicationType ApplicationType = clsApplicationType.Find(5); // 5 = Application Release
        public clsLicenses License = new clsLicenses();
        public clsDetainedLicense DetainLicense = new clsDetainedLicense();
        public clsApplication Application = new clsApplication();

        public frmReleaseDetainedLicense()
        {
            
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            
            License = clsLicenses.Find(LicenseID);
            

            InitializeComponent();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            _LoadData();

            ucFilterDriverLicenseInfo1.DataFound += UcFilterDriverLicenseInfo1_DataFound;

            if (License.LicenseID != -1)
            {

                ucFilterDriverLicenseInfo1.LoadDataForUpdateMode(License.LicenseID);

            }

        }

        private void UcFilterDriverLicenseInfo1_DataFound(object sender, clsLicenses License)
        {
            if (License.LicenseID == -1)
            {
                DefaultDetainInfo();

                llblShowLicenseInfo.Enabled = false;
                llblShowLicenseHistory.Enabled = false;
                return;
            }

            this.License = License;
            lblLicenseID.Text = License.LicenseID.ToString();


            llblShowLicenseInfo.Enabled = false;
            llblShowLicenseHistory.Enabled = true;

            if (!clsDetainedLicense.IsDetainLicenseExistsByLicenseID(License.LicenseID))
            {
                btnRelease.Enabled = false;
                
                MessageBox.Show("Selected License is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            DetainLicense = clsDetainedLicense.FindByLicenseID(License.LicenseID);


            if (DetainLicense != null)
            {
                lblDetainID.Text         = DetainLicense.DetainID.ToString();
                lblDetainDate.Text       = DetainLicense.DetainDate.ToString("dd/MMM/yyyy");
                lblApplicationFees.Text  = ApplicationType.ApplicationFees.ToString("0");
                lblFineFees.Text         = DetainLicense.FineFees.ToString("0");
                lblCreatedBy.Text        = clsUser.Find(DetainLicense.CreatedByUserID).UserName;

                lblTotalFees.Text = (ApplicationType.ApplicationFees + DetainLicense.FineFees).ToString("0");

            }
            else
            {
                MessageBox.Show("return null \n { DetainLicense }", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);


                return;
            }



           

            btnRelease.Enabled = true;


        }

        private void DefaultDetainInfo()
        {
            lblLicenseID.Text = "[????]";
            lblDetainID.Text =         "[????]";
            lblDetainDate.Text =       "[??/???/????]";
            lblApplicationFees.Text =  "[$$$$]";
            lblFineFees.Text =         "[$$$$]";
            lblCreatedBy.Text =        "[????]";
            lblTotalFees.Text = "[$$$$]";

        }

        private void _LoadData()
        {
            
           
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = License.Application.ApplicantPersonID;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(PersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LicenseID = License.LicenseID;

            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(LicenseID, false);
            frmLicenseInfo.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            DoReleaseLicense();
        }


        private void DoReleaseLicense()
        {
            var Result = MessageBox.Show("Are you sure you want to Release this Detained license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;

            if (DoReleaseLicenseApplication())
            {
            
              DetainLicense.IsReleased = true;
              DetainLicense.ReleaseDate = DateTime.Now;
              DetainLicense.ReleasedByUserID = frmLogin.user.UserID;
              DetainLicense.ReleaseApplicationID = Application.ApplicationID;
             
              if (DetainLicense.Save())
              {
                  lblApplicationID.Text = Application.ApplicationID.ToString();
                  btnRelease.Enabled = false;
                  llblShowLicenseInfo.Enabled = true;
                  MessageBox.Show("Detained License Released Successfully ", "License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ucFilterDriverLicenseInfo1.DataFound -= UcFilterDriverLicenseInfo1_DataFound;
                    ucFilterDriverLicenseInfo1.LoadDataForUpdateMode(License.LicenseID);
             
              }
              else
              {
                  MessageBox.Show("Error : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  return;
             
              }
            
            }
            else
            {
                MessageBox.Show("Error Application : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


        }

        private bool DoReleaseLicenseApplication()
        {

            //Base Application
            Application.ApplicantPersonID = License.Application.Person.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = ApplicationType.ApplicationTypeID;
            Application.ApplicationStatus = 3;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = ApplicationType.ApplicationFees;
            Application.CreatedByUserID = frmLogin.user.UserID;

            return Application.Save();


        }



    }



}
