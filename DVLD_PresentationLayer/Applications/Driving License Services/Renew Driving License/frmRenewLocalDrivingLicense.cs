using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
using Driving___Vehicle_License_Department__DVLD_.UserControls;
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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.Renew_Driving_License
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        private int _NewLicenseID = -1;
      
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            ucFilterDriverLicenseInfo1.FilterFocus();

            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblExpirationDate.Text = "[??/???/????]";
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString("0");
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.DriverInfo.PersonID;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(PersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            var Result = MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;

            clsLicense NewLicense = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Error : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _NewLicenseID = NewLicense.LicenseID;

            lblR_L_ApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblIRenewedLicenseID.Text = _NewLicenseID.ToString();

            MessageBox.Show($"License Renewed Successfully with ID = {_NewLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            btnRenew.Enabled = false;
            ucFilterDriverLicenseInfo1.FilterEnabled = false;
            llblShowNewLicenseInfo.Enabled = true;

        }

        private void llblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
           frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_NewLicenseID);
           frmLicenseInfo.ShowDialog();

        }

        private void ucFilterDriverLicenseInfo1_OnLicenseSelected(int LicenseID)
        {
            lblOldLicenseID.Text = LicenseID.ToString();
            llblShowLicenseHistory.Enabled = (LicenseID != -1);

            if (LicenseID == -1)
            {
                lblOldLicenseID.Text = "[????]";
                return;
            }

            clsLicense License = ucFilterDriverLicenseInfo1.SelectedLicenseInfo;

          
            lblExpirationDate.Text = DateTime.Now.AddYears(License.LicenseClassInfo.DefaultValidityLength).ToString("dd/MMM/yyyy");

            lblLicenseFees.Text = License.LicenseClassInfo.ClassFees.ToString("0");

            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();

            txtNotes.Text = License.Notes;



            if (!License.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, Choose an active license", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenew.Enabled = false;
                return;
            }

            if (License.IsNotExpired())
            {
                MessageBox.Show($"Selected License is not yet expire, it will expire on :\n{License.ExpirationDate.ToString("dd/MMM/yyyy")}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenew.Enabled = false;
                return;
            }


            btnRenew.Enabled = true;
        }
    
    }
}
