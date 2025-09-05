using DVLD_BusinessLayer;
using System;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.Replacement_for_Lost_or_Damaged_License
{
    public partial class frmReplacementForDamagedLicenses : Form
    {
        clsApplicationType ApplicationType = clsApplicationType.Find(4); // 4 = Damaged License // 3 = Lost License
        public clsLicenses OldLicense = new clsLicenses();
        public clsLicenses License = new clsLicenses();
        public clsApplication Application = new clsApplication();

        public frmReplacementForDamagedLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReplacementForDamagedLicenses_Load(object sender, EventArgs e)
        {
            _LoadData();
            ucFilterDriverLicenseInfo1.DataFound += UcFilterDriverLicenseInfo1_DataFound;
        }

        private void UcFilterDriverLicenseInfo1_DataFound(object sender, clsLicenses License)
        {
            if (License.LicenseID == -1)
            {
                lblOldLicenseID.Text = "[????]";
                this.OldLicense = License;
                //llblShowLicenseInfo.Enabled = false;
                llblShowLicenseHistory.Enabled = false;
                return;
            }

            this.OldLicense = License;
            lblOldLicenseID.Text = License.LicenseID.ToString();

            llblShowLicenseHistory.Enabled = true;

            if (!this.OldLicense.IsActive)
            {
                btnIssueReplacement.Enabled = false;

                MessageBox.Show("Selected License is Not Active, Choose an active license", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnIssueReplacement.Enabled = true;
            


        }

        private void _LoadData()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = frmLogin.user.UserName;

            lblApplicationFees.Text = ApplicationType.ApplicationFees.ToString("0");

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            // 4 = Damaged License // 3 = Lost License

            if (rbDamagedLicense.Checked)
            {
                ApplicationType = clsApplicationType.Find(4);
                lblMode.Text = "Replacement For Damaged Licenses";
                
            }
            else
            { 
                ApplicationType = clsApplicationType.Find(3);
                lblMode.Text = "Replacement For Lost Licenses";
            }

            this.Text = lblMode.Text;

            lblApplicationFees.Text = ApplicationType.ApplicationFees.ToString("0");

        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = OldLicense.Application.ApplicantPersonID;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(PersonID);
            frmLicenseHistory.ShowDialog();
        }

        
        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            DoIssueReplacementLicense();
        }


        private void DoIssueReplacementLicense()
        {
            var Result = MessageBox.Show("Are you sure you want to issue a replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;

            if (DoReplacementApplication())
            {
                License.ApplicationID = Application.ApplicationID;
                License.LicenseClass = OldLicense.LicenseClass;
                License.CreatedByUserID = frmLogin.user.UserID;

                if (License.Save())
                {
                    OldLicense.IsActive = false;

                    if (OldLicense.Save())
                    {
                        lblL_R_ApplicationID.Text = Application.ApplicationID.ToString();
                        lblReplacedLicenseID.Text = License.LicenseID.ToString();

                        btnIssueReplacement.Enabled = false;

                        llblShowNewLicenseInfo.Enabled = true;

                        MessageBox.Show($"Licensed Replaced Successfully with ID= {License.LicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        ucFilterDriverLicenseInfo1.DataFound -= UcFilterDriverLicenseInfo1_DataFound;
                        ucFilterDriverLicenseInfo1.LoadDataForUpdateMode(OldLicense.LicenseID);
                        gbReplacementFor.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Error : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                }

            }


        }

        private bool DoReplacementApplication()
        {

            //Base Application
            Application.ApplicantPersonID = OldLicense.Application.Person.PersonID;
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
            int LicenseID = (License.LicenseID == -1) ? OldLicense.LicenseID : License.LicenseID;


            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(LicenseID, false);
            frmLicenseInfo.ShowDialog();
        }
    }
}
