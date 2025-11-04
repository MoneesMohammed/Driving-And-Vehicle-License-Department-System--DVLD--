using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.International_License.International_Driver_Info;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        
        private int _InternationalLicenseID = -1;
        
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            _LoadData();
           
        }

        private void _LoadData()
        {

            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString("0");

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            var Result = MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            InternationalLicense.DriverID = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ucFilterDriverLicenseInfo1.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1) ;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Error : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblI_L_LicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            lblI_L_ApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;

            MessageBox.Show($"International License Issued Successfully with ID = {InternationalLicense.InternationalLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ucFilterDriverLicenseInfo1.FilterEnabled = false;
            llblShowLicenseInfo.Enabled = true;

        }



        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.DriverInfo.PersonID;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(PersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            frmInternationalDriverInfo frmInternationalDriverInfo = new frmInternationalDriverInfo(_InternationalLicenseID);
            frmInternationalDriverInfo.ShowDialog();
        }

        private void ucFilterDriverLicenseInfo1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            llblShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                lblLocalLicenseID.Text = "[????]";
                
                return;
            }

            clsLicense License = ucFilterDriverLicenseInfo1.SelectedLicenseInfo;

            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(License.DriverID);

            if (ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show($"Person already has an active international license with ID = {ActiveInternationalLicenseID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                _InternationalLicenseID = ActiveInternationalLicenseID;
                btnIssue.Enabled = false;
                return;
            }
           

            if (License.LicenseClass != 3 )
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            if (!License.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, Choose an active license", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssue.Enabled = false;
                return;
            }

            if (!License.IsNotExpired())
            {
                MessageBox.Show($"Selected License is expire, It expired on:\n{License.ExpirationDate.ToString("dd/MMM/yyyy")}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssue.Enabled = false;
                return;
            }


            btnIssue.Enabled = true;
        }
    }
}
