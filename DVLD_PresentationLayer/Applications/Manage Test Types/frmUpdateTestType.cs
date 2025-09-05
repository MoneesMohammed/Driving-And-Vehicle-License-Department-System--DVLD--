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
        private int _TestTypeID;
        clsTestType _TestType;

        public frmUpdateTestType(int TestTypeID)
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


            if (_TestType == null)
                return;

            lblID.Text          = _TestType.TestTypeID.ToString();
            txtTitle.Text       = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text        = _TestType.TestTypeFees.ToString();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _TestType.TestTypeTitle       = txtTitle.Text;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeFees        = Convert.ToDecimal(txtFees.Text);


            if (_TestType.Save())
            {

                MessageBox.Show("Data saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
