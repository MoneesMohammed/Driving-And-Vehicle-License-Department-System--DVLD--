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
    public partial class frmDetainLicense : Form
    {

        public clsLicenses License = new clsLicenses();
        public clsDetainedLicense DetainLicense = new clsDetainedLicense();

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {

            _LoadData();
            ucFilterDriverLicenseInfo1.DataFound += UcFilterDriverLicenseInfo1_DataFound;
        }

        private void UcFilterDriverLicenseInfo1_DataFound(object sender, clsLicenses License)
        {
            if (License.LicenseID == -1)
            {
                lblLicenseID.Text = "[????]";

                llblShowLicenseInfo.Enabled = false;
                llblShowLicenseHistory.Enabled = false;
                return;
            }

            this.License = License;
            lblLicenseID.Text = License.LicenseID.ToString();


            llblShowLicenseInfo.Enabled    = true;
            llblShowLicenseHistory.Enabled = true;

            if (!this.License.IsActive)
            {
                btnDetain.Enabled = false;

                MessageBox.Show("Selected License is Not Active, Choose an active license", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (clsDetainedLicense.IsDetainLicenseExistsByLicenseID(License.LicenseID))
            { 
            
              
              MessageBox.Show("Selected License already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return;
                
            }


            btnDetain.Enabled = true;

        }

        private void _LoadData()
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = frmLogin.user.UserName;

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

        private void btnDetain_Click(object sender, EventArgs e)
        {
            DoDetainLicense();
        }


        private void DoDetainLicense()
        {
            var Result = MessageBox.Show("Are you sure you want to Detain for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;

            DetainLicense.LicenseID = License.LicenseID;
            DetainLicense.FineFees = Convert.ToDecimal(txtFineFees.Text);
            DetainLicense.CreatedByUserID = frmLogin.user.UserID;


            if (DetainLicense.Save())
            {
                lblDetainID.Text = DetainLicense.DetainID.ToString();
  
                btnDetain.Enabled = false;

                MessageBox.Show($"Licens Detained Successfully with ID= {DetainLicense.DetainID}", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                ucFilterDriverLicenseInfo1.DataFound -= UcFilterDriverLicenseInfo1_DataFound;
                ucFilterDriverLicenseInfo1.LoadDataForUpdateMode(License.LicenseID);
                
            }
            else
            {
                MessageBox.Show("Error : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }





        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
