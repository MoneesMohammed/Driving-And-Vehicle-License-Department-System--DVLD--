using Driving___Vehicle_License_Department__DVLD_.UserControls;
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

        private int _PersonID = -1;

        public frmLicenseHistory()
        {
            InitializeComponent();
           
        }

        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ucFilterPerson1.LoadPersonInfo(_PersonID);
                ucFilterPerson1.FilterEnabled = false;
                ucDriverLicenses1.LoadInfoByPersonID(_PersonID);

            }
            else
            {
                ucFilterPerson1.FilterEnabled = true;
                ucFilterPerson1.FilterFocus();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucFilterPerson1_OnPersonSelected(int obj)
        {
            _PersonID = obj;

            if (_PersonID == -1)
            { ucDriverLicenses1.Clear();}
            else
            { ucDriverLicenses1.LoadInfoByPersonID(_PersonID); }
            
        }
    }
}
