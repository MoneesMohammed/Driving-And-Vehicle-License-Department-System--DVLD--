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

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Applications.Local_Driving_License_Applications.Show_Person_License_History
{
    public partial class frmLicenseHistory : Form
    {
        //private int _LDLAppID;

        //public clsLocalDrivingLicenseApplication LDLAppApplication = new clsLocalDrivingLicenseApplication();

        //public frmLicenseHistory(int LDLAppID)
        //{
        //    InitializeComponent();
        //    _LDLAppID = LDLAppID;

        //    LDLAppApplication = clsLocalDrivingLicenseApplication.Find(LDLAppID);
        //}

        int PersonID;

        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {
            
            ucFilterPerson1.LoadDataForUpdateMode(this.PersonID);

            ucDriverLicenses1.RefreshUCDriverLicenses(this.PersonID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
