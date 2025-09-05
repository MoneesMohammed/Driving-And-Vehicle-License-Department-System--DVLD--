using Driving___Vehicle_License_Department__DVLD_.Properties;
using Driving___Vehicle_License_Department__DVLD_.UserControls;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
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
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;


        private int _PersonID;
        private clsPerson _Person;

        public int PersonID { get; set; }

        public frmAddEditPersonInfo(int PersonID )
        {
            InitializeComponent();

            _PersonID = PersonID;
            this.PersonID = PersonID;

            if (PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;


        }



        private void _FillCountryInComboBox()
        {
            DataTable dt = clsCountry.GetAllCountries();

            foreach (DataRow dr in dt.Rows)
            {
                cbCountry.Items.Add(dr["CountryName"]);

            }


        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _LoadData();


        }

        private void _LoadData()
        {
            _FillCountryInComboBox();
            cbCountry.SelectedIndex = 89;

            btnSave.Enabled = false;

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            pbPerson.Image = Resources.man;

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
                return;
            }

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show($"this form will be closed because no Person with ID Found {_PersonID}");
                this.Close();
                return;
            }

            lblMode.Text = "Update Person";

            lblPersonID.Text = _Person.PersonID.ToString();

            txtNationalNo.Text = _Person.NationalNo;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
            {
                rbMale.Checked = true;

            }
            else if (_Person.Gendor == 1)
            {
                rbFemale.Checked = true;

            }




            if (_Person.ImagePath != "")
            {
                pbPerson.Image = LoadImageWithoutLock(_Person.ImagePath);
            }

            llblRemove.Visible = (_Person.ImagePath != "");



            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);
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

            int CountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;

            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Address = txtAddress.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = CountryID;
            _Person.NationalNo = txtNationalNo.Text;



            if (rbMale.Checked)
            {
                _Person.Gendor = 0;
            }
            else if (rbFemale.Checked)
            {
                _Person.Gendor = 1;
            }

            //Copy and rename then grab the file path and send it to the database.

            if (llblRemove.Visible == false && _Person.ImagePath != "")
            {
                File.Delete(_Person.ImagePath);
                _Person.ImagePath = "";
            }
            else if (pbPerson.ImageLocation != null && pbPerson.ImageLocation != _Person.ImagePath)
            {
                if (_Person.ImagePath != "")
                {
                    File.Delete(_Person.ImagePath);

                }

                _Person.ImagePath = _GetPathFileCopied(@"C:\DVLD-People-Images", pbPerson.ImageLocation); ;
            }
            else
            {
                _Person.ImagePath = _Person.ImagePath;
            }



            if (_Person.Save())
            {

                MessageBox.Show("Data saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            _Mode = enMode.Update;

            lblMode.Text = "Update Person";
            lblPersonID.Text = _Person.PersonID.ToString();

            this.PersonID = _Person.PersonID;

        }

        private string _GetPathFileCopied(string DestinationFolder, string SourcePath)
        {

            if (!Directory.Exists(DestinationFolder))
            {
                Directory.CreateDirectory(DestinationFolder);
            }

            Guid guid = Guid.NewGuid();

            string extension = Path.GetExtension(SourcePath);

            string FileName = guid.ToString() + extension;

            string DestinationPath = Path.Combine(DestinationFolder, FileName);

            try
            {
                File.Copy(SourcePath, DestinationPath, true);
                MessageBox.Show("Image copied successfully ✅");
                return DestinationPath;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You do not have permission to access this path. Try running the program as administrator or choose a different folder..");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while copying : " + ex.Message, "error");
            }



            return "";
        }


        private void llblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.DefaultExt = "JPG";

            openFileDialog1.Filter = "JPG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                pbPerson.LoadAsync(openFileDialog1.FileName);

            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (!llblRemove.Visible)
                pbPerson.Image = rbMale.Checked ? Resources.man : Resources.woman;
        }

        private void llblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            pbPerson.ImageLocation = null;
            llblRemove.Visible = false;
            pbPerson.Image = rbMale.Checked ? Resources.man : Resources.woman;

        }

        private void pbPerson_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            llblRemove.Visible = true;
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                e.Cancel = true;
                //txtFirstName.Focus();
                errorProvider1.SetError(txtFirstName, "First Name should have a Value!");
               
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFirstName, "");
               
            }



        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text))
            {
                e.Cancel = true;
                //txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National Number should have a Value!");
               

            }
            else if (clsPerson.IsPersonExists(txtNationalNo.Text) && txtNationalNo.Text != _Person.NationalNo.ToString())
            {
                e.Cancel = true;
               // txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National Number is used by another Person");
                btnSave.Enabled = false;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, "");
                if (!_OneOftxtBoxIsEmpty())
                    btnSave.Enabled = true;
            }





        }


        private bool _IsCorrectEmail(string Email)
        {
            if (Email.Contains("@gmail.com"))
                return true;
            else if (Email.Contains("@icloud.com"))
                return true;
            else if (Email.Contains("@outlook.com"))
                return true;
            else if (Email.Contains("@yahoo.com"))
                return true;


            return false;

        }



        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || _IsCorrectEmail(txtEmail.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");

                if(!_OneOftxtBoxIsEmpty())
                     btnSave.Enabled = true;

            }
            else if (!_IsCorrectEmail(txtEmail.Text))
            {
                e.Cancel = true;
               // txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Invalid Email!");
                btnSave.Enabled = false;
            }


        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                e.Cancel = true;
                //txtAddress.Focus();
                errorProvider1.SetError(txtAddress, "Address should have a Value!");
                
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAddress, "");
                
            }
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSecondName.Text))
            {
                e.Cancel = true;
                //txtSecondName.Focus();
                errorProvider1.SetError(txtSecondName, "Second Name should have a Value!");
                
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtSecondName, "");
                
            }

        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                e.Cancel = true;
               // txtSecondName.Focus();
                errorProvider1.SetError(txtLastName, "Last Name should have a Value!");
               
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLastName, "");
                
            }
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                e.Cancel = true;
                //txtSecondName.Focus();
                errorProvider1.SetError(txtPhone, "Phone should have a Value!");

                
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPhone, "");
                
            }
        }

        private bool _OneOftxtBoxIsEmpty()
        {

            bool OneOfIsEmpty = string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                               string.IsNullOrWhiteSpace(txtSecondName.Text) ||
                               string.IsNullOrWhiteSpace(txtLastName.Text) ||
                               string.IsNullOrWhiteSpace(txtNationalNo.Text) ||
                               string.IsNullOrWhiteSpace(txtPhone.Text) ||
                               string.IsNullOrWhiteSpace(txtAddress.Text);

            return OneOfIsEmpty;
        }

        private void AllTextBoxes_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender ;

            bool IsPersonExists = clsPerson.IsPersonExists(txtNationalNo.Text) && txtNationalNo.Text != _Person.NationalNo.ToString();

           //||  errorProvider1.GetError(txtEmail) != ""

            if (_OneOftxtBoxIsEmpty() || IsPersonExists )
            {
                if (IsPersonExists)
                {
                    txtNationalNo.Focus();
                    errorProvider1.SetError(txtNationalNo, "National Number is used by another Person");

                }
                else
                {
                    errorProvider1.SetError(txtNationalNo, "");
                }

                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
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
