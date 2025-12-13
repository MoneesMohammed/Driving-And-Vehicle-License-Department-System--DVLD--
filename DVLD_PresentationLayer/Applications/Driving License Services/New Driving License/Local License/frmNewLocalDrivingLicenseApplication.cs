using DVLD.Classes;
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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Driving_License_Services.New_Driving_License.Local_License
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _SelectedPersonID = -1 ;
        private int _LocalLDApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalLDApplication ;
        
        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmNewLocalDrivingLicenseApplication(int LocalLDApplicationID)
        {
            InitializeComponent();
            _LocalLDApplicationID = LocalLDApplicationID;
            _Mode = enMode.Update;
        }

        private void _GoToTabApplicationInfo()
        {
            tabControl1.Selecting -= tabControl1_Selecting;  // Temporarily unblock
            tabControl1.SelectedTab = tabApplicationInfo;           // or any other tab
            tabControl1.Selecting += tabControl1_Selecting;  // Reblock

            btnSave.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                _GoToTabApplicationInfo();
                return;
            }

            if (ucFilterPerson1.PersonID == -1)
            {
                MessageBox.Show($"There is no person.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                
            }

            _GoToTabApplicationInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(tabControl1.SelectedTab != tabPersonalInfo)
            e.Cancel = true;
        }


        private void _FillLicenseClassInComboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }

        }


        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValue();

            if (_Mode == enMode.Update)
            _LoadData();
        }

        private void _ResetDefualtValue()
        {
            _FillLicenseClassInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalLDApplication = new clsLocalDrivingLicenseApplication();
               //ucFilterPerson1.FilterFocus();

                //tabApplicationInfo.Enabled = false;
                cbLicenseClass.SelectedIndex = 2;
                lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString("00");
                lblApplicationDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblMode.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tabControl1.Enabled = true;
                btnSave.Enabled     = true;

            }
            
        }


        private void _LoadData()
        {
            ucFilterPerson1.FilterEnabled = false;
            _LocalLDApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(_LocalLDApplicationID);

            if (_LocalLDApplication == null)
            {
                MessageBox.Show($"No Application With ID = {_LocalLDApplicationID}", "No Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ucFilterPerson1.LoadPersonInfo(_LocalLDApplication.ApplicantPersonID);
            lblDLApplicationID.Text = _LocalLDApplicationID.ToString();
            lblApplicationDate.Text = _LocalLDApplication.ApplicationDate.ToString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(_LocalLDApplication.LicenseClassInfo.ClassName);
            lblApplicationFees.Text = _LocalLDApplication.PaidFees.ToString();
            lblCreatedBy.Text = clsUser.FindByUserID(_LocalLDApplication.CreatedByUserID).UserName;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID,clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose Another License Class, The Selected Person Already Have An Active Application.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (clsLocalDrivingLicenseApplication.IsExistsApplicationByStatus(_SelectedPersonID.NationalNo, cbLicenseClass.Text))
            //{
            //    MessageBox.Show("Choose another License Class, the selected Person Already have an active application.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else if (clsLocalDrivingLicenseApplication.IsExistsApplicationByStatus(_SelectedPersonID.NationalNo, cbLicenseClass.Text, "Completed"))
            //{

            //    MessageBox.Show("Person already has a license with the same applied driving class. Choose a different driving class.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;

            //}

            if (clsLicense.IsLicenseExistsByPersonID(_SelectedPersonID, LicenseClassID))
            {
                MessageBox.Show("Person already has a license with the same applied driving class. Choose a different driving class.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (clsLicenseClass.Find(LicenseClassID).MinimumAllowedAge > clsPerson.Find(_SelectedPersonID).GetAge())
            {

                MessageBox.Show("The person age is not suitable for this class of license. Choose a different driving class.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            //Base Application
            _LocalLDApplication.ApplicantPersonID = _SelectedPersonID;
            _LocalLDApplication.ApplicationDate   = DateTime.Now;
            _LocalLDApplication.ApplicationTypeID = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationTypeID;
            _LocalLDApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalLDApplication.LastStatusDate    = DateTime.Now;
            _LocalLDApplication.PaidFees          = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees;
            _LocalLDApplication.CreatedByUserID   = clsGlobal.CurrentUser.UserID;

            //Local Driving License Application
            _LocalLDApplication.LicenseClassID    = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

            if (_LocalLDApplication.Save())
            {
                
              lblDLApplicationID.Text = _LocalLDApplication.LocalDrivingLicenseApplicationID.ToString();
              lblMode.Text = "Update Local Driving License Application";
              _Mode = enMode.Update;

              MessageBox.Show("Data saved successfully", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }
            else
            {
              MessageBox.Show("Error : data is not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

        }


        private void ucFilterPerson1_OnPersonSelected(int obj)
        { 

         _SelectedPersonID = obj;
        
        }

        private void frmNewLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ucFilterPerson1.FilterFocus();
        }
    }
}
