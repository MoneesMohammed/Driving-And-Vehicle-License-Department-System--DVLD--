using DVLD_BusinessLayer;
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

        private int _ApplicationTypeID;
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


            if (_ApplicationType == null)
                return;

            lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            _ApplicationType.ApplicationTypeTitle = txtTitle.Text ;
            _ApplicationType.ApplicationFees = Convert.ToDecimal( txtFees.Text) ;


            if (_ApplicationType.Save())
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
