using Driving___Vehicle_License_Department__DVLD_.Properties;
using DVLD.Classes;
using DVLD_BusinessLayer;
using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_DataAccessLayar.clsTestType;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications
{
    public partial class frmTakeTest : Form
    {
        private clsTestType.enTestType _TestTypeID = enTestType.VisionTest;
        private int _TestAppointmentID = -1;

        private clsTest _Test;
        private int _TestID = -1;

        public frmTakeTest(int TestAppointmentID )
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
           
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ucScheduledTest1.LoadInfo(_TestAppointmentID);

            if (ucScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            int _TestID = ucScheduledTest1.TestID;

            if (_TestID != -1)
            {
                _Test = clsTest.Find(_TestID);

                if (_Test.TestResult)
                    rbSuccess.Enabled = true;
                else
                    rbFailed.Enabled = false;

                txtNotes.Text = _Test.Notes;

                lblMessError.Visible = true;
                rbFailed.Enabled = false;
                rbSuccess.Enabled = false;

            }
            else
            {
                _Test = new clsTest();

            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            var Result = MessageBox.Show("Are you sure you want to save? After that, you cannot change the Pass/Fail results after you save?", "Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
          
            if (Result == DialogResult.No)
                return;

            

            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = rbSuccess.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if (_Test.Save())
            {
                ucScheduledTest1.LoadTestID(_Test.TestID);

                btnSave.Enabled = false;
               
                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully for Test", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }


    }
}
