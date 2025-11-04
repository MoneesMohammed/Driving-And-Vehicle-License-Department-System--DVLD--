using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_License;
using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History;
using DVLD.Classes;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Detain_Licenses
{
    public partial class frmDetainLicense : Form
    {
        private int _SelectedLicenseID = -1;
        private int _DetainID = -1;

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

            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.DriverInfo.PersonID;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(PersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_SelectedLicenseID);
            frmLicenseInfo.ShowDialog();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            var Result = MessageBox.Show("Are you sure you want to Detain for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;

            _DetainID = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.Detain(Convert.ToDecimal(txtFineFees.Text), clsGlobal.CurrentUser.UserID);


            if (_DetainID != -1)
            {
                lblDetainID.Text = _DetainID.ToString();

                MessageBox.Show($"License Detained Successfully with ID= {_DetainID}", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnDetain.Enabled = false;
                ucFilterDriverLicenseInfo1.FilterEnabled = false;
                txtFineFees.Enabled = false;
                llblShowLicenseInfo.Enabled = true;

            }
            else
            {
                MessageBox.Show("Failed To Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


        }

        private void ucFilterDriverLicenseInfo1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llblShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
            {
                lblLicenseID.Text = "[No License Selected]";
                btnDetain.Enabled = false;
                return;
            }


            if (!ucFilterDriverLicenseInfo1.SelectedLicenseInfo.IsActive)
            {
                btnDetain.Enabled = false;

                MessageBox.Show("Selected License is Not Active, Choose an active license", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (ucFilterDriverLicenseInfo1.SelectedLicenseInfo.IsDetained)
            {

                btnDetain.Enabled = false;
                MessageBox.Show("Selected License already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
      
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtFineFees, "This Field Is Required!");

            }
            else
            {
                
                errorProvider1.SetError(txtFineFees, null);

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
