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

namespace Driving___Vehicle_License_Department__DVLD_.UserControls
{
    public partial class ucDriverInternationalLicenseInfo : UserControl
    {
        clsInternationalLicense Int_License = new clsInternationalLicense();

        public ucDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }


        public void RefreshUCDriverInternationalLicenseInfo(int Int_LicenseID)
        {
            Int_License = clsInternationalLicense.Find(Int_LicenseID);

            if (Int_License == null)
            {
                DefaultUCDriverLicenseInfo();
                return;
            }


            lblName.Text = (Int_License.Application.Person.ThirdName == "") ?
                $"{Int_License.Application.Person.FirstName} {Int_License.Application.Person.SecondName} {Int_License.Application.Person.LastName}" :
                $"{Int_License.Application.Person.FirstName} {Int_License.Application.Person.SecondName} {Int_License.Application.Person.ThirdName} {Int_License.Application.Person.LastName}";

            lblInt_LicenseID.Text = Int_LicenseID.ToString();
            lblLicenseID.Text = Int_License.LicenseID.ToString();
            lblNationalNo.Text = Int_License.Application.Person.NationalNo;
            

            if (Int_License.Application.Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
                lblGendor1.Image = Resources.Male;
            }
            else
            {
                lblGendor.Text = "Female";
                lblGendor1.Image = Resources.Female;
            }

            lblIssueDate.Text = Int_License.IssueDate.ToString("dd/MMM/yyyy");

            lblExpiationDate.Text = Int_License.ExpirationDate.ToString("dd/MMM/yyyy");

           
            lblDateOfBirth.Text = Int_License.Application.Person.DateOfBirth.ToString("dd/MMM/yyyy");

            lblApplicationID.Text = Int_License.ApplicationID.ToString();

            lblIsActive.Text = (Int_License.IsActive) ? "Yes" : "No";

            lblDriverID.Text = Int_License.License.DriverID.ToString();


            if (Int_License.Application.Person.ImagePath != "")
            {
                pbPerson.Image = LoadImageWithoutLock(Int_License.Application.Person.ImagePath);
            }
            else
            {
                pbPerson.Image = Int_License.Application.Person.Gendor == 0 ? Resources.man : Resources.woman;
            }


        }


        private void DefaultUCDriverLicenseInfo()
        {

            lblName.Text = "[????]";
            lblInt_LicenseID.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";

            lblGendor.Text = "Male";
            lblGendor1.Image = Resources.Male;

            lblIssueDate.Text = "[????]";
            lblApplicationID.Text = "[????]";
           

            lblIsActive.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblExpiationDate.Text = "[????]";
            
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
