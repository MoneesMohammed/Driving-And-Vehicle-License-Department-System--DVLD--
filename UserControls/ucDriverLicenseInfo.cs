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

        public clsLicenses Licenses = new clsLicenses();


        public ucDriverLicenseInfo()
        {
            InitializeComponent();

        }

        
        public void RefreshUCDriverLicenseInfo(int LDLAppID)
        {
            int ApplicationID = clsLocalDrivingLicenseApplication.Find(LDLAppID).ApplicationID;

            Licenses = clsLicenses.FindByApplicationID(ApplicationID);

            _LoadUCDriverLicense();


        }

        public void RefreshUCDriverLicenseInfoByLicenseID(int LicenseID)
        {
            Licenses = clsLicenses.Find(LicenseID);

            _LoadUCDriverLicense();

        }


        private void _LoadUCDriverLicense()
        {
            if (Licenses == null)
            {
                DefaultUCDriverLicenseInfo();
                return;
            }

            lblClass.Text = Licenses.LicenseClassInfo.ClassName;

            lblName.Text = (Licenses.Application.Person.ThirdName == "") ?
                $"{Licenses.Application.Person.FirstName} {Licenses.Application.Person.SecondName} {Licenses.Application.Person.LastName}" :
                $"{Licenses.Application.Person.FirstName} {Licenses.Application.Person.SecondName} {Licenses.Application.Person.ThirdName} {Licenses.Application.Person.LastName}";

            lblLicenseID.Text = Licenses.LicenseID.ToString();
            lblNationalNo.Text = Licenses.Application.Person.NationalNo;


            if (Licenses.Application.Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
                lblGendor1.Image = Resources.Male;
            }
            else
            {
                lblGendor.Text = "Female";
                lblGendor1.Image = Resources.Female;
            }


            lblIssueDate.Text = Licenses.IssueDate.ToString("dd/MMM/yyyy");

            byte IssueReason = Licenses.IssueReason;


            lblIssueReason.Text = (IssueReason == 1) ? "First Time" : (IssueReason == 2) ? "Renew" :
                (IssueReason == 3) ? "Replacement for Damaged" : (IssueReason == 4) ? "Replacement for Lost" : "";

            //-_- IssueReason -_-  // 3 = Damaged License // 4 = Lost License


            lblNotes.Text = (Licenses.Notes == "") ? "No Notes" : Licenses.Notes;

            lblIsActive.Text = (Licenses.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = Licenses.Application.Person.DateOfBirth.ToString("dd/MMM/yyyy");
            lblDriverID.Text = Licenses.DriverID.ToString();
            lblExpiationDate.Text = Licenses.ExpirationDate.ToString("dd/MMM/yyyy");

            bool IsDetained = clsDetainedLicense.IsDetainLicenseExistsByLicenseID(Licenses.LicenseID);

            lblIsDetained.Text = (IsDetained) ? "Yes" : "No";




            //label23 = 459, 348 / lblIsDetained = 683, 348

            if (IssueReason == 3)
            {
                label23.Location = new System.Drawing.Point(543, 348);
                lblIsDetained.Location = new System.Drawing.Point(767, 348);

            }
            else if (IssueReason == 4)
            {
                label23.Location = new System.Drawing.Point(475, 348);
                lblIsDetained.Location = new System.Drawing.Point(699, 348);
            }
            else
            {
                label23.Location = new System.Drawing.Point(459, 348);
                lblIsDetained.Location = new System.Drawing.Point(683, 348);

            }


            if (Licenses.Application.Person.ImagePath != "")
            {
                pbPerson.Image = LoadImageWithoutLock(Licenses.Application.Person.ImagePath);
            }
            else
            {
                pbPerson.Image = Licenses.Application.Person.Gendor == 0 ? Resources.man : Resources.woman;
            }


        }




        private void DefaultUCDriverLicenseInfo()
        {

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

        private System.Drawing.Image LoadImageWithoutLock(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return System.Drawing.Image.FromStream(fs);
            }
        }

    }
}
