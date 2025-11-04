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
        private int _InternationalLicenseID = -1;
        private clsInternationalLicense _InternationalLicense;

        public int InternationalLicenseID { get { return _InternationalLicenseID; } }
        public clsInternationalLicense SelectedInternationalLicenseInfo { get { return _InternationalLicense; } }

        public ucDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }


        public void LoadInfo(int InternationalLicenseID)
        {

            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                ResetInternationalLicenseInfo();
                MessageBox.Show($"Could Not Found International LicenseID ={InternationalLicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillInternationalLicense();

        }


        private void _FillInternationalLicense()
        {
            clsPerson Person = clsPerson.Find(_InternationalLicense.ApplicantPersonID);

            lblName.Text = _InternationalLicense.ApplicantFullName;
            lblInt_LicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = Person.NationalNo;


            if (Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
                lblGendor1.Image = Resources.Male;
            }
            else
            {
                lblGendor.Text = "Female";
                lblGendor1.Image = Resources.Female;
            }

            lblIssueDate.Text = _InternationalLicense.IssueDate.ToString("dd/MMM/yyyy");

            lblExpiationDate.Text = _InternationalLicense.ExpirationDate.ToString("dd/MMM/yyyy");

            lblDateOfBirth.Text = Person.DateOfBirth.ToString("dd/MMM/yyyy");

            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();

            lblIsActive.Text = (_InternationalLicense.IsActive) ? "Yes" : "No";

            lblDriverID.Text = _InternationalLicense.DriverID.ToString();


            _LoadPersonImage(Person);

        }


        private void _LoadPersonImage(clsPerson Person)
        {
            pbPerson.Image = Person.Gendor == 0 ? Resources.man : Resources.woman;

            string ImagePath = Person.ImagePath;

            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPerson.Load(ImagePath);
                else
                    MessageBox.Show($"Could Not Find This Image : = {ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ResetInternationalLicenseInfo()
        {
            _InternationalLicenseID = -1;

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


       


    }
}
