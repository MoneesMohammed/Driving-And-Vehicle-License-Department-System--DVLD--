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
        public clsPerson Person = new clsPerson();
       

        public ucFilterPerson()
        {
            InitializeComponent();
            
        }

        private void _RefreshUCPersonDetails(int PersonID = -1)
        {

            ucPersonDetails1.RefreshUCPersonDetails(PersonID);


        }

        public void LoadDataForUpdateMode(int PersonID)
        {
            Person = clsPerson.Find(PersonID);

            if(Person == null)
                return;


            gbFind.Enabled = false;
            cbFindBy.SelectedIndex = 1;
            txtFindBy.Text = Person.PersonID.ToString();
            ucPersonDetails1.RefreshUCPersonDetails(Person.PersonID);


        }


        private void ucFilterPerson_Load(object sender, EventArgs e)
        {
            cbFindBy.SelectedIndex = 0;

        }


        private void btnSearchPerson_Click(object sender, EventArgs e)
        {
            if (txtFindBy.Text == string.Empty)
            {
                _RefreshUCPersonDetails();
                MessageBox.Show("How do I search for a person without their information?\nThe box is empty!\nPlease fill in the appropriate information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (cbFindBy.SelectedIndex == 1 && int.TryParse(txtFindBy.Text, out int ID))
                Person = clsPerson.Find(ID);
            else if (cbFindBy.SelectedIndex == 0)
                Person = clsPerson.Find(txtFindBy.Text);



            if (Person == null)
            {
                _RefreshUCPersonDetails();
                MessageBox.Show($"Don't Found Person by {cbFindBy.SelectedItem} : {txtFindBy.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _RefreshUCPersonDetails(Person.PersonID);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddEditPerson = new frmAddEditPersonInfo(-1);
            frmAddEditPerson.ShowDialog();

            int ID = frmAddEditPerson.PersonID;

            if (ID == -1)
                return;

            cbFindBy.SelectedIndex = 1;
            txtFindBy.Text = ID.ToString();

            Person = clsPerson.Find(ID);

            _RefreshUCPersonDetails(Person.PersonID);
        }

        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            if (cbFindBy.SelectedIndex == 1 && int.TryParse(txtFindBy.Text, out int ID))
                Person = clsPerson.Find(ID);
            else if (cbFindBy.SelectedIndex == 0)
                Person = clsPerson.Find(txtFindBy.Text);

            if (Person == null)
                return;


            _RefreshUCPersonDetails(Person.PersonID);

        }
    }
}
