using Driving___Vehicle_License_Department__DVLD_.Properties;
using Driving___Vehicle_License_Department__DVLD_.UserControls;
using DVLD_BusinessLayer;
using System;
using DVLD.Classes;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Driving___Vehicle_License_Department__DVLD_
{
    public partial class frmAddEditPersonInfo : Form
    {
        //Delegate
        public delegate void DataFoundEventHandler(object sender, int PersonID);

        public event DataFoundEventHandler DataBack;


        public enum enMode { AddNew = 0, Update = 1 }
        public enum enGendor {Male = 0 , Female = 1 }

        private enMode _Mode ;
        private int _PersonID = -1;
        clsPerson _Person;

        //public int PersonID { get; set; }

        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }


        public frmAddEditPersonInfo(int PersonID )
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;

        }

        private void _FillCountryInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);

            }


        }

        private void _ResatDefualtValues()
        {

            _FillCountryInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblMode.Text = "Update Person";
            }

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.man;
            else
                pbPersonImage.Image = Resources.woman;


            llblRemove.Visible = (pbPersonImage.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Jordan");


            txtFirstName.Text    = "";
            txtSecondName.Text   = "";
            txtThirdName.Text    = "";
            txtLastName.Text     = "";
            txtNationalNo.Text   = "";
            rbMale.Checked       = true;
            txtEmail.Text        = "";
            txtPhone.Text        = "";
            txtAddress.Text      = "";
            

        }


        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _ResatDefualtValues();

            if(_Mode == enMode.Update)
                _LoadData();


        }

        private void _LoadData()
        {

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show($"No Person with ID= {_PersonID} ","Person Not Found ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }


            lblPersonID.Text = _Person.PersonID.ToString();

            txtNationalNo.Text      = _Person.NationalNo;
            txtFirstName.Text       = _Person.FirstName;
            txtSecondName.Text      = _Person.SecondName;
            txtThirdName.Text       = _Person.ThirdName;
            txtLastName.Text        = _Person.LastName;
            txtEmail.Text           = _Person.Email;
            txtPhone.Text           = _Person.Phone;
            txtAddress.Text         = _Person.Address;
            dtpDateOfBirth.Value    = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
            {
               rbMale.Checked = true;
            }
            else 
            {
              rbFemale.Checked = true;
            }

            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);


            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }

            llblRemove.Visible = (_Person.ImagePath != "");
            
        }

        private Image LoadImageWithoutLock(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(fs);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!_HandlePersonImage())
                return;

            int CountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;

            _Person.NationalNo = txtNationalNo.Text;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Address = txtAddress.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = CountryID;



            if (rbMale.Checked)
            {
                _Person.Gendor = 0;
            }
            else if (rbFemale.Checked)
            {
                _Person.Gendor = 1;
            }

            
            if (pbPersonImage.ImageLocation != null)
            {
               _Person.ImagePath = pbPersonImage.ImageLocation;
            }
            else
            {
               _Person.ImagePath = "";
            }

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;
                lblMode.Text = "Update Person";


                MessageBox.Show("Data saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);

            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

           

        }

      
        private void llblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.DefaultExt = "JPG";

            openFileDialog1.Filter = "JPG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                pbPersonImage.Load(openFileDialog1.FileName);
                llblRemove.Visible = false;
            }
        }

        private void llblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            pbPersonImage.Image = rbMale.Checked ? Resources.man : Resources.woman;

            llblRemove.Visible = false;
        }
       
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (!llblRemove.Visible)
                pbPersonImage.Image = rbMale.Checked ? Resources.man : Resources.woman;
        }

        private bool _HandlePersonImage()
        {

            // Copy and rename then grab the file path and send it to the database.
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        //Log 
                    }
                    
                    
                }

            }

            if (pbPersonImage.ImageLocation != null)
            {
                string sourceImageFile = pbPersonImage.ImageLocation.ToString();

                if (clsUtil.CopyImageToProjectImagesFolder(ref sourceImageFile))
                {
                    pbPersonImage.ImageLocation = sourceImageFile;

                    return true;
                }
                else
                {

                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }

            }
            
             return true;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Tepm = ((TextBox)sender);

            if (string.IsNullOrEmpty(Tepm.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(Tepm, "this field is required!");

            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Tepm, null);

            }



        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                return;

            //Validate Email Format
            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else 
            {
               errorProvider1.SetError(txtEmail, null);
            }

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number should have a Value!");
            }
            else if (clsPerson.IsPersonExists(txtNationalNo.Text) && txtNationalNo.Text.Trim() != _Person.NationalNo)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used by another Person");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }


        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
