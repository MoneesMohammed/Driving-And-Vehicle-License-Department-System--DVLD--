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

        public clsPerson Person = new clsPerson();
        private int _PersonID;

        public ucPersonDetails()
        {
            InitializeComponent();

        }

        public ucPersonDetails(int PersonID)
        {
            InitializeComponent();

            this._PersonID = PersonID;

           
        }

        public void RefreshUCPersonDetails(int PersonID)
        {
            if (PersonID == -1)
                DefaultucPersonDetails(); 

            Person = clsPerson.Find(PersonID);

            if (Person == null)
                return;

            llblEditPersonInfo.Enabled = true;

            lblPersonID.Text = Person.PersonID.ToString();
            
            lblName.Text = (Person.ThirdName == "") ?
                $"{Person.FirstName} {Person.SecondName} {Person.LastName}" :
                $"{Person.FirstName} {Person.SecondName} {Person.ThirdName} {Person.LastName}";

            lblNationalNo.Text = Person.NationalNo;

            if (Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
                lblGendor.Image = Resources.Male;
            }
            else
            {
                lblGendor.Text = "Female";
                lblGendor.Image = Resources.Female;
            }

            lblEmail.Text = Person.Email;
            lblPhone.Text = Person.Phone;
            lblDateOfBirth.Text = Person.DateOfBirth.ToString("MM/dd/yyyy"); 
            lblAddress.Text = Person.Address;

            lblcountry.Text = clsCountry.Find(Person.NationalityCountryID).CountryName;

            if (Person.ImagePath != "")
            {
                pbPerson.Image = LoadImageWithoutLock(Person.ImagePath);
            }
            else
            {
                pbPerson.Image = Person.Gendor == 0 ? Resources.man : Resources.woman;
            }

        }


        private void ucPersonDetails_Load(object sender, EventArgs e)
        {
            _LoadData();
            
        }

        private void DefaultucPersonDetails()
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



        private void _LoadData()
        {
            
            RefreshUCPersonDetails(_PersonID);

        }

        private Image LoadImageWithoutLock(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(fs);
            }
        }

        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frmAddEditPersonInfo = new frmAddEditPersonInfo(Person.PersonID);
            frmAddEditPersonInfo.ShowDialog();

            RefreshUCPersonDetails(Person.PersonID);
        }

        
    }
}
