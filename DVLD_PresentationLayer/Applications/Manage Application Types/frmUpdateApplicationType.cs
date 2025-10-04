using DVLD_BusinessLayer;
using DVLD.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Application_Types
{
    public partial class frmUpdateApplicationType : Form
    {

        private int _ApplicationTypeID = -1;
        clsApplicationType _ApplicationType;

        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            if (_ApplicationType != null)
            { 
                lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
                txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
                txtFees.Text = _ApplicationType.ApplicationFees.ToString();

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

            _ApplicationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            _ApplicationType.ApplicationFees = Convert.ToDecimal( txtFees.Text.Trim()) ;


            if (_ApplicationType.Save())
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
