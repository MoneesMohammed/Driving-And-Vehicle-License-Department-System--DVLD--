using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Driving___Vehicle_License_Department__DVLD_.UserControls
{
    public partial class ucFilterPerson : UserControl
    {
        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            { 
               handler(PersonID);
            }

        }

        private bool _ShowAddPerson = true;

        public bool ShowAddPerson
        { 
            
            get { return _ShowAddPerson; } 
           
            set 
            { 
                _ShowAddPerson = value; 
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {

            get { return _FilterEnabled; }

            set
            {
                _ShowAddPerson = value;
                gbFind.Visible = _FilterEnabled;
            }
        }

        private int _PersonID=-1;

        public int PersonID
        {
            get { return ucPersonDetails1.PersonID; }

        }


        public clsPerson SelectedPersonInfo
        {
            get { return ucPersonDetails1.SelectedPersonInfo; }
        
        }
       

        public ucFilterPerson()
        {
            InitializeComponent();
            
        }

        

        public void LoadPersonInfo(int PersonID)
        {
            
            cbFindBy.SelectedIndex = 1;
            txtFindBy.Text = PersonID.ToString();

            FindNow();
        }

        private void FindNow()
        {

            if (cbFindBy.SelectedIndex == 1 && int.TryParse(txtFindBy.Text, out int ID))
               ucPersonDetails1.LoadPersonInfo(ID);
            else if (cbFindBy.SelectedIndex == 0)
               ucPersonDetails1.LoadPersonInfo(txtFindBy.Text);


            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected (ucPersonDetails1.PersonID);

        }

        private void ucFilterPerson_Load(object sender, EventArgs e)
        {
            cbFindBy.SelectedIndex = 0;
            txtFindBy.Focus();
        }


        private void btnSearchPerson_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindNow();
        }

        

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddEditPerson = new frmAddEditPersonInfo();
            frmAddEditPerson.DataBack += frmAddEditPerson_DataBack;
            frmAddEditPerson.ShowDialog();

           

        }

        private void frmAddEditPerson_DataBack(object sender,int PersonID )
        {
            cbFindBy.SelectedIndex = 1;
            txtFindBy.Text = PersonID.ToString();

            ucPersonDetails1.LoadPersonInfo(PersonID);

        }


        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            //if (cbFindBy.SelectedIndex == 1 && int.TryParse(txtFindBy.Text, out int ID))
            //    ucPersonDetails1.LoadPersonInfo(ID);
            //else if (cbFindBy.SelectedIndex == 0)
            //    ucPersonDetails1.LoadPersonInfo(txtFindBy.Text);

            //if (SelectedPersonInfo == null)
            //    return;


        }

        private void txtFindBy_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFindBy.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFindBy, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtFindBy, null);

            }
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSearchPerson.PerformClick();
            }

            if (cbFindBy.SelectedIndex == 1)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
