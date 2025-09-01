using Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.International_License.International_Driver_Info;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
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
        clsApplicationType ApplicationType = clsApplicationType.Find(6); // 6 = New International License
        public clsLicenses License = new clsLicenses();
        public clsInternationalLicense InternationalLicense = new clsInternationalLicense();

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
            ucFilterDriverLicenseInfo1.DataFound += UcFilterDriverLicenseInfo1_DataFound;
        }

        private void UcFilterDriverLicenseInfo1_DataFound(object sender, clsLicenses License)
        {
            if (License.LicenseID == -1)
            {
                lblLocalLicenseID.Text = "[????]";
                this.License = License;
                //llblShowLicenseInfo.Enabled = false;
                llblShowLicenseHistory.Enabled = false;
                return;
            }
            
           
            this.License = License;
            lblLocalLicenseID.Text = License.LicenseID.ToString();


            InternationalLicense = clsInternationalLicense.FindByLicenseID(License.LicenseID);

            if (InternationalLicense != null)
            {
                //lblI_L_LicenseID.Text     = InternationalLicense.InternationalLicenseID.ToString();
                //lblI_L_ApplicationID.Text = InternationalLicense.ApplicationID.ToString();

                //lblApplicationDate.Text = InternationalLicense.Application.ApplicationDate.ToString("dd/MMM/yyyy");
                //lblIssueDate.Text = InternationalLicense.IssueDate.ToString("dd/MMM/yyyy");
                //lblExpirationDate.Text = InternationalLicense.ExpirationDate.ToString("dd/MMM/yyyy");

                if(InternationalLicense.IsActive)
                   MessageBox.Show($"Person already have an active international license with ID = {InternationalLicense.InternationalLicenseID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);


                llblShowLicenseInfo.Enabled = true;
                btnIssue.Enabled = false;
            }
            else
            {

                lblI_L_LicenseID.Text     = "[????]";
                lblI_L_ApplicationID.Text = "[????]";


                //lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                //lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                //lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");

                llblShowLicenseInfo.Enabled = false;
                InternationalLicense = new clsInternationalLicense();

                btnIssue.Enabled = true;
            }

            
            if (License.LicenseClass == 3 && License.IsActive && License.IsNotExpired() && InternationalLicense.LicenseID == -1)
            {
                
                 btnIssue.Enabled = true;

              
            }
            else
            {
                

                btnIssue.Enabled = false;
            }

            llblShowLicenseHistory.Enabled = true;
            //llblShowLicenseInfo.Enabled = true;

        }

        private void _LoadData()
        {

            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = frmLogin.user.UserName;

            lblFees.Text = ApplicationType.ApplicationFees.ToString("0");

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            DoIssue();
        }


        private void DoIssue()
        {

            var Result = MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (Result == DialogResult.No)
                return;

            InternationalLicense.LicenseID = License.LicenseID;
            InternationalLicense.CreatedByUserID = frmLogin.user.UserID;

            if (InternationalLicense.Save())
            {
                lblI_L_LicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
                lblI_L_ApplicationID.Text = InternationalLicense.ApplicationID.ToString();

                llblShowLicenseInfo.Enabled = true;
                btnIssue.Enabled = false;

                MessageBox.Show($"International License Issued Successfully with ID = {InternationalLicense.InternationalLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            


        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = License.Application.ApplicantPersonID;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(PersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int Int_LicenseID = InternationalLicense.InternationalLicenseID;
            frmInternationalDriverInfo frmInternationalDriverInfo = new frmInternationalDriverInfo(Int_LicenseID);
            frmInternationalDriverInfo.ShowDialog();
        }

        private void gbApplicationInfo_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ucFilterDriverLicenseInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
