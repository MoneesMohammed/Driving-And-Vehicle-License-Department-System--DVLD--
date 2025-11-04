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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Detain_Licenses
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private int _SelectedLicenseID = -1;
        
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            if (_SelectedLicenseID != -1)
            {
                ucFilterDriverLicenseInfo1.LoadLicenseInfo(_SelectedLicenseID);
                ucFilterDriverLicenseInfo1.FilterEnabled = false;
            }

        }

        
        private void DefaultDetainInfo()
        {
            lblLicenseID.Text       = "[????]";
            lblDetainID.Text        = "[????]";
            lblDetainDate.Text      = "[??/???/????]";
            lblApplicationFees.Text = "[$$$$]";
            lblFineFees.Text        = "[$$$$]";
            lblCreatedBy.Text       = "[????]";
            lblTotalFees.Text       = "[$$$$]";

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

        private void btnRelease_Click(object sender, EventArgs e)
        {
            var Result = MessageBox.Show("Are you sure you want to Release this Detained license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;


            int ApplicationID=-1;

            bool IsReleased = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);


            if (IsReleased)
            {
                lblApplicationID.Text = ApplicationID.ToString();

                MessageBox.Show("Detained License Released Successfully ", "License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnRelease.Enabled = false;
                ucFilterDriverLicenseInfo1.FilterEnabled = false;
                llblShowLicenseInfo.Enabled = true;

            }
            else
            {
                MessageBox.Show("Error : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DefaultDetainInfo();
                btnRelease.Enabled = false;
                return;
            }

            if (!ucFilterDriverLicenseInfo1.SelectedLicenseInfo.IsDetained)
            {
                btnRelease.Enabled = false;

                MessageBox.Show("Selected License is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsDetainedLicense DetainLicense = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.DetainedInfo;

            
            lblDetainID.Text        = DetainLicense.DetainID.ToString();
            lblDetainDate.Text      = DetainLicense.DetainDate.ToString("dd/MMM/yyyy");
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString("0");
            lblFineFees.Text        = DetainLicense.FineFees.ToString("0");
            lblCreatedBy.Text       = clsGlobal.CurrentUser.UserName;
            lblTotalFees.Text       = (Convert.ToDecimal(lblApplicationFees.Text) + DetainLicense.FineFees).ToString("0");
           
            btnRelease.Enabled = true;

        }
    }



}
