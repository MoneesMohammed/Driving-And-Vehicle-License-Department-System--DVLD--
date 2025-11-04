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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.Replacement_for_Lost_or_Damaged_License
{
    public partial class frmReplacementForDamagedLicenses : Form
    {

        private clsLicense.enIssueReason _IssueReason;
        private clsApplicationType _ApplicationType;
        private int _NewLicenseID = -1;

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
            ucFilterDriverLicenseInfo1.FilterFocus();

            _LoadData();
            
        }

        private void _LoadData()
        {
            _IssueReason = clsLicense.enIssueReason.ReplacementForDamaged;
            _ApplicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplacementDamagedDrivingLicense);

            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            lblApplicationFees.Text = _ApplicationType.ApplicationFees.ToString("0");
            rbDamagedLicense.Checked = true;
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.DriverInfo.PersonID;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(PersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            var Result = MessageBox.Show("Are you sure you want to issue a replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.No)
                return;


            clsLicense NewLicense = ucFilterDriverLicenseInfo1.SelectedLicenseInfo.Replace(_IssueReason, clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Error : data is not saved successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _NewLicenseID = NewLicense.LicenseID;

            lblL_R_ApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblReplacedLicenseID.Text = _NewLicenseID.ToString();


            MessageBox.Show($"Licensed Replaced Successfully with ID= {_NewLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            ucFilterDriverLicenseInfo1.FilterEnabled = false;
            llblShowNewLicenseInfo.Enabled = true;
        }

       
        private void llblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_NewLicenseID);
            frmLicenseInfo.ShowDialog();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            // 4 = Damaged License // 3 = Lost License
            

            if (rbDamagedLicense.Checked)
            {
                _ApplicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplacementDamagedDrivingLicense);
                _IssueReason = clsLicense.enIssueReason.ReplacementForDamaged;
                lblMode.Text = "Replacement For Damaged Licenses";

            }
            else
            {
                _ApplicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplacementLostDrivingLicense);
                _IssueReason = clsLicense.enIssueReason.ReplacementForLost;
                lblMode.Text = "Replacement For Lost Licenses";
            }

            this.Text = lblMode.Text;

            lblApplicationFees.Text = _ApplicationType.ApplicationFees.ToString("0");

        }


        private void ucFilterDriverLicenseInfo1_OnLicenseSelected(int obj)
        {

            int SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llblShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                lblOldLicenseID.Text = "[????]";
                return;
            }

            if (ucFilterDriverLicenseInfo1.SelectedLicenseInfo == null)
            { return; }


            if (!ucFilterDriverLicenseInfo1.SelectedLicenseInfo.IsActive)
            {

                MessageBox.Show("Selected License is Not Active, Choose an active license", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
        }
    }
}
