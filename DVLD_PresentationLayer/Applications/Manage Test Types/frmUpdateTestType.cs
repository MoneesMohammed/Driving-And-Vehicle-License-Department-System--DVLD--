using DVLD.Classes;
using DVLD_BusinessLayer;
using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Test_Types
{
    public partial class frmUpdateTestType : Form
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _TestType;

        public frmUpdateTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {
            _TestType = clsTestType.Find(_TestTypeID);


            if (_TestType != null)
            {
                lblID.Text = _TestType.TestTypeID.ToString();
                txtTitle.Text = _TestType.TestTypeTitle;
                txtDescription.Text = _TestType.TestTypeDescription;
                txtFees.Text = _TestType.TestTypeFees.ToString("00");
            }
            else
            {
                MessageBox.Show($"Colud Not Find Test Type With ID = {_TestTypeID.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _TestType.TestTypeTitle       = txtTitle.Text;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeFees        = Convert.ToDecimal(txtFees.Text);


            if (_TestType.Save())
            {

                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtTitle, "Title Cannot Be Empty!");

            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtTitle, null);

            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtDescription, "Title Cannot Be Empty!");

            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtDescription, null);

            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtFees, "Fees Cannot Be Empty!");

            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFees, null);

            }


            if (!clsValidatoin.IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtFees, "Invalid Number.");

            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFees, null);

            }
        }
    }
}
