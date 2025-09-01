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
        public clsPerson Person = new clsPerson();
        public clsApplicationType ApplicationType = clsApplicationType.Find(1);

        public clsLocalDrivingLicenseApplication LocalLDApplication = new clsLocalDrivingLicenseApplication();
        public clsApplication Application = new clsApplication();

        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();

            
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
            Person = ucFilterPerson1.Person;

            
            if (Person == null || Person.PersonID == -1)
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
            //e.Cancel = true;
        }


        private void _FillLicenseClassInComboBox()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow dr in dt.Rows)
            {
                cbLicenseClass.Items.Add(dr["ClassName"]);
        
            }

            cbLicenseClass.SelectedIndex = 0;

        }


        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {
            _FillLicenseClassInComboBox();
            //lblApplicationFees.Text = ApplicationType.ApplicationFees.ToString();

            btnSave.Enabled = false;

            lblCreatedBy.Text = frmLogin.user.UserName;
            lblApplicationDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (tabControl1.SelectedTab != tabApplicationInfo)
                return;

            if (clsLocalDrivingLicenseApplication.IsExistsApplicationByStatus(Person.NationalNo, cbLicenseClass.Text))
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (clsLocalDrivingLicenseApplication.IsExistsApplicationByStatus(Person.NationalNo, cbLicenseClass.Text, "Completed"))
            {

                MessageBox.Show("Person already has a license with the same applied driving class. Choose a different driving class.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            
            //Base Application
            Application.ApplicantPersonID = Person.PersonID;
            Application.ApplicationDate   = DateTime.Now;
            Application.ApplicationTypeID = ApplicationType.ApplicationTypeID;
            Application.ApplicationStatus = 1;
            Application.LastStatusDate    = DateTime.Now;
            Application.PaidFees          = ApplicationType.ApplicationFees;
            Application.CreatedByUserID   = frmLogin.user.UserID;

            if (Application.Save())
            {
                //Local Driving License Application

                int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

                LocalLDApplication.ApplicationID = Application.ApplicationID;
                LocalLDApplication.LicenseClassID = LicenseClassID;


                if (LocalLDApplication.Save())
                {
                    lblDLApplicationID.Text = LocalLDApplication.LocalDrivingLicenseApplicationID.ToString();

                    MessageBox.Show("Data saved successfully", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    MessageBox.Show("Error : data is not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

                MessageBox.Show("Error : data is not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }



            lblMode.Text = "Update Local Driving License Application";




        }



    }
}
