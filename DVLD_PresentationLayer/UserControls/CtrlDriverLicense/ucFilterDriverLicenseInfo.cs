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
using static Driving___Vehicle_License_Department__DVLD_.UserControls.ucFilterDriverLicenseInfo;

namespace Driving___Vehicle_License_Department__DVLD_.UserControls
{
    public partial class ucFilterDriverLicenseInfo : UserControl
    {
       
        public event Action<int> OnLicenseSelected;

        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }

        }

        private bool _ShowAddLicense = true;

        public bool ShowAddLicense
        {

            get { return _ShowAddLicense; }

            set
            {
                _ShowAddLicense = value;
                btnSearchLicense.Visible = _ShowAddLicense;
            }
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {

            get { return _FilterEnabled; }

            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }


        private int _LicenseID = -1;
        private clsLicense _License;

        public int LicenseID { get { return ucDriverLicenseInfo1.LicenseID; } }
        public clsLicense SelectedLicenseInfo { get { return ucDriverLicenseInfo1.SelectedLicenseInfo; } }

        public ucFilterDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public void FilterFocus()
        {
            txtFilterByID.Focus();

        }

        public void LoadLicenseInfo(int LicenseID)
        {
            txtFilterByID.Text = LicenseID.ToString();

            ucDriverLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ucDriverLicenseInfo1.LicenseID;
            _License = ucDriverLicenseInfo1.SelectedLicenseInfo;


            if (OnLicenseSelected != null && FilterEnabled)
                OnLicenseSelected(_LicenseID);
        }

        private void btnSearchLicense_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FilterFocus();
                return;
            }


            _LicenseID = int.Parse(txtFilterByID.Text);

            LoadLicenseInfo(_LicenseID);

        }

        

        private void txtFilterByID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)13)
            {

                btnSearchLicense.PerformClick();

            }

            
            
        }

        private void txtFilterByID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterByID.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtFilterByID, "This Field Is Required!");

            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilterByID, null);

            }
        }
    }


}
