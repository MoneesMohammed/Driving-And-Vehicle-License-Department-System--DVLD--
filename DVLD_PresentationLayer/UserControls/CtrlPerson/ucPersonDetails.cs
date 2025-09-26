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
    public partial class ucPersonDetails : UserControl
    {

        private clsPerson _Person = new clsPerson();
        private int _PersonID = -1;

        public int PersonID 
        { 
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }


        public ucPersonDetails()
        {
            InitializeComponent();

        }


        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
              ResetPersonInfo();
              MessageBox.Show($"No Person With PersonID ={PersonID}" ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
              return;
            }

            _FillPersonInfo();

        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show($"No Person With National No. ={NationalNo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _FillPersonInfo();

        }

        private void _FillPersonInfo()
        {
            llblEditPersonInfo.Enabled = true;

            lblPersonID.Text = _Person.PersonID.ToString();

            lblName.Text = (_Person.ThirdName == "") ?
                $"{_Person.FirstName} {_Person.SecondName} {_Person.LastName}" :
                $"{_Person.FirstName} {_Person.SecondName} {_Person.ThirdName} {_Person.LastName}";

            lblNationalNo.Text = _Person.NationalNo;

            if (_Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
                lblGendor.Image = Resources.Male;
            }
            else
            {
                lblGendor.Text = "Female";
                lblGendor.Image = Resources.Female;
            }

            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("MM/dd/yyyy");
            lblAddress.Text = _Person.Address;

            lblcountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;

            _LoadPersonImage();

        }

        private void ucPersonDetails_Load(object sender, EventArgs e)
        {
            
            
        }

        private void ResetPersonInfo()
        {

            llblEditPersonInfo.Enabled = false;

            lblPersonID.Text = "[???]";
            lblName.Text = "[???]";
            lblNationalNo.Text = "[???]";

            lblGendor.Text = "Male";
            lblGendor.Image = Resources.Male;
            
            lblEmail.Text = "[???]";
            lblPhone.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblAddress.Text = "[???]";

            lblcountry.Text = "[???]";

            pbPerson.Image = Resources.man;

        }

        private void _LoadPersonImage()
        {
            string ImagePath = _Person.ImagePath;

            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPerson.Image = _LoadImageWithoutLock(ImagePath);
                else
                    MessageBox.Show($"Could Not Find This Image : = {ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                pbPerson.Image = _Person.Gendor == 0 ? Resources.man : Resources.woman;
            }
        }


        private Image _LoadImageWithoutLock(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(fs);
            }
        }

        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_Person.PersonID);
            frm.ShowDialog();

            LoadPersonInfo(_Person.PersonID);
        }

        
    }
}
