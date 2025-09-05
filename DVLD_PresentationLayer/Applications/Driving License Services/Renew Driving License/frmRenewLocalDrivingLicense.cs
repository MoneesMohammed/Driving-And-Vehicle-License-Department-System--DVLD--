using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.Renew_Driving_License
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        clsApplicationType ApplicationType = clsApplicationType.Find(2); // 2 =  Renew Local License
        public clsLicenses Old_License = new clsLicenses();
        public clsLicenses License = new clsLicenses();
        public clsApplication Application = new clsApplication();


        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _LoadData();
            ucFilterDriverLicenseInfo1.DataFound += UcFilterDriverLicenseInfo1_DataFound;
        }

        private void UcFilterDriverLicenseInfo1_DataFound(object sender, clsLicenses License)
        {
            if (License.LicenseID == -1)
            {
                lblOldLicenseID.Text = "[????]";
                this.Old_License = License;
                
                llblShowLicenseHistory.Enabled = false;
                return;
            }

            this.Old_License = License;
            lblOldLicenseID.Text = License.LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(this.Old_License.LicenseClassInfo.DefaultValidityLength).ToString("dd/MMM/yyyy");

           lblLicenseFees.Text = this.Old_License.PaidFees.ToString("0");

           lblTotalFees.Text = (int.Parse(lblApplicationFees.Text) + int.Parse(lblLicenseFees.Text)).ToString("0");




            if (License.IsActive)
            {
                btnRenew.Enabled = true;

                if (License.IsNotExpired())
                {
                    MessageBox.Show($"Selected License is not yet expire, it will expire on :\n{License.ExpirationDate.ToString("dd/MMM/yyyy")}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btnRenew.Enabled = false;

                }

            }
            else
            {
                btnRenew.Enabled = false;

                MessageBox.Show("Selected License is Not Active, Choose an active license", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }




            
            

           

            llblShowLicenseHistory.Enabled = true;


        }


        private void _LoadData()
        {

            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblExpirationDate.Text = DateTime.Now.AddYears(10).ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = frmLogin.user.UserName;

            lblApplicationFees.Text = ApplicationType.ApplicationFees.ToString("0");

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = Old_License.Application.ApplicantPersonID;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(PersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            DoRenewLicense();
        }

        private void DoRenewLicense()
        {
            var Result = MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;

            if (DoRenewApplication())
            {

                License.ApplicationID   = Application.ApplicationID;
                License.LicenseClass    = Old_License.LicenseClass;
                License.CreatedByUserID = frmLogin.user.UserID;

                if (License.Save())
                { 
                    Old_License.IsActive = false;

                    if (Old_License.Save())
                    {
                        lblR_L_ApplicationID.Text = Application.ApplicationID.ToString();
                        lblIRenewedLicenseID.Text = License.LicenseID.ToString();

                        btnRenew.Enabled = false;

                        llblShowNewLicenseInfo.Enabled = true;

                        MessageBox.Show($"License Renewed Successfully with ID = {License.LicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Error : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }



                }


            }




        }

        private bool DoRenewApplication()
        {
            //Base Application
            Application.ApplicantPersonID = Old_License.Application.Person.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = ApplicationType.ApplicationTypeID;
            Application.ApplicationStatus = 3;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = ApplicationType.ApplicationFees;
            Application.CreatedByUserID = frmLogin.user.UserID;

            return Application.Save();

        }

       

        private void llblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           int LicenseID = (License.LicenseID == -1) ? Old_License.LicenseID : License.LicenseID;
            
           frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(LicenseID , false);
           frmLicenseInfo.ShowDialog();


        }
    }
}
