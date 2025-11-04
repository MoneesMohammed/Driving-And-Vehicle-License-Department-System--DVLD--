using Driving___Vehicle_License_Department__DVLD_.Properties;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Driving___Vehicle_License_Department__DVLD_.UserControls
{
    public partial class ucDriverLicenseInfo : UserControl
    {
        private int _LicenseID = -1;
        private clsLicense _License;

        public int LicenseID { get { return _LicenseID; }}
        public clsLicense SelectedLicenseInfo { get { return _License; } }
        

        public ucDriverLicenseInfo()
        {
            InitializeComponent();

        }

        

        public void LoadInfo(int LicenseID)
        {
            _License = clsLicense.Find(LicenseID);

            if (_License == null)
            {
                ResetDriverLicenseInfo();
                MessageBox.Show($"Could Not Found LicenseID ={LicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillDriverLicense();

        }


        private void _FillDriverLicense()
        {
            _LicenseID = _License.LicenseID;

            lblClass.Text = _License.LicenseClassInfo.ClassName;

            lblName.Text = _License.DriverInfo.PersonInfo.FullName;

            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;


            if (_License.DriverInfo.PersonInfo.Gendor == 0)
            {
                lblGendor.Text = "Male";
                lblGendor1.Image = Resources.Male;
            }
            else
            {
                lblGendor.Text = "Female";
                lblGendor1.Image = Resources.Female;
            }

            lblIssueDate.Text = _License.IssueDate.ToString("dd/MMM/yyyy");


            lblIssueReason.Text = _License.IssueReasonText;

            //-_- IssueReason -_-  FirstTime = 1, Renew = 2 , ReplacementForDamaged = 3, ReplacementForLost = 4


            lblNotes.Text = (_License.Notes == "") ? "No Notes" : _License.Notes;

            lblIsActive.Text = (_License.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpiationDate.Text = _License.ExpirationDate.ToString("dd/MMM/yyyy");

            lblIsDetained.Text = (_License.IsDetained) ? "Yes" : "No";

            


            //label23 = 459, 348 / lblIsDetained = 683, 348

            if (_License.IssueReason == clsLicense.enIssueReason.ReplacementForDamaged)
            {
                label23.Location = new System.Drawing.Point(543, 348);
                lblIsDetained.Location = new System.Drawing.Point(767, 348);

            }
            else if (_License.IssueReason == clsLicense.enIssueReason.ReplacementForLost)
            {
                label23.Location = new System.Drawing.Point(475, 348);
                lblIsDetained.Location = new System.Drawing.Point(699, 348);
            }
            else
            {
                label23.Location = new System.Drawing.Point(459, 348);
                lblIsDetained.Location = new System.Drawing.Point(683, 348);

            }


            _LoadPersonImage();


        }

        private void _LoadPersonImage()
        {
            pbPerson.Image = _License.DriverInfo.PersonInfo.Gendor == 0 ? Resources.man : Resources.woman;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPerson.Load(ImagePath);
                else
                    MessageBox.Show($"Could Not Find This Image : = {ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        private void ResetDriverLicenseInfo()
        {
            _LicenseID = -1;

            lblClass.Text      = "[????]";
            lblName.Text       = "[????]";
            lblLicenseID.Text  = "[????]";
            lblNationalNo.Text = "[????]";

            lblGendor.Text = "Male";
            lblGendor1.Image = Resources.Male;

            lblIssueDate.Text   = "[????]";
            lblIssueReason.Text = "[????]";
            lblNotes.Text       = "[????]";

            lblIsActive.Text      = "[????]";
            lblDateOfBirth.Text   = "[????]";
            lblDriverID.Text      = "[????]";
            lblExpiationDate.Text = "[????]";
            lblIsDetained.Text    = "[????]";


            pbPerson.Image = Resources.man;

        }




        //private System.Drawing.Image _LoadImageWithoutLock(string filePath)
        //{
        //    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        return System.Drawing.Image.FromStream(fs);
        //    }
        //}

    }
}
