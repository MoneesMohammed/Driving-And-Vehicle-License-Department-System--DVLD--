using DVLD_BusinessLayer;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using static Driving___Vehicle_License_Department__DVLD_.frmManagePeople;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Driving___Vehicle_License_Department__DVLD_
{
    public partial class frmManagePeople : Form
    {

        private DataTable _dtPeople = clsPerson.GetAllPeople_1();

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();

            cbFilterBy.SelectedIndex = 0;
        }

        private void _RefreshPeopleList()
        {
           
            dgvAllPeople.DataSource = _dtPeople;

            lblRecodes.Text = dgvAllPeople.Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddEditPersonInfo = new frmAddEditPersonInfo();
            frmAddEditPersonInfo.ShowDialog();

            _RefreshPeopleList();

        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddEditPersonInfo = new frmAddEditPersonInfo((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frmAddEditPersonInfo.ShowDialog();

            _RefreshPeopleList();


        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvAllPeople.CurrentRow.Cells[0].Value;

            //string ImagePath = clsPerson.GetImagePath(PersonID);

            
          var result = MessageBox.Show($"Are you sure you want to delete the Person by PersonID: {PersonID}", "Warning", MessageBoxButtons.OKCancel ,MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(PersonID))
                {
                    //if (ImagePath != "")
                    //    File.Delete(ImagePath);

                    MessageBox.Show("Person has been deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                   


            } 
            
            _RefreshPeopleList();
        }

        private void tsmAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddEditPersonInfo = new frmAddEditPersonInfo();
            frmAddEditPersonInfo.ShowDialog();

            _RefreshPeopleList();
        }

        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be available soon.", "Send Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsmPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be available soon.", "Phone Call", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (cbFilterBy.Text == "None")
            {
                txtFilterBy.Visible = false;
                _RefreshPeopleList();
            }
            else
            {
                txtFilterBy.Visible = true;
            }

            txtFilterBy.MaxLength = (cbFilterBy.Text == "Phone") ? 10 : 32767;

        }
        
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = cbFilterBy.Text;
           
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecodes.Text = dgvAllPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "Person ID" || FilterColumn == "Phone")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn,txtFilterBy.Text.Trim()); //[FilterColumn] = txtFilterBy.Text
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());
                     //[FilterColumn] LIKE 'txtFilterBy.Text%'

            lblRecodes.Text = dgvAllPeople.Rows.Count.ToString();
        }


        private void FilterBy(DataTable PeopleDataTable, DataRow[] ResultRows, string Select )
        {
            if (txtFilterBy.Text != "")
            {
                ResultRows = PeopleDataTable.Select(Select);

                if (ResultRows.Length > 0)
                {
                    dgvAllPeople.DataSource = ResultRows.CopyToDataTable();

                    lblRecodes.Text = ResultRows.Count().ToString();

                }
                else
                {
                    dgvAllPeople.DataSource = null;

                    lblRecodes.Text = "0";
                }
            }
            else
            {
                dgvAllPeople.DataSource = null;
                lblRecodes.Text = "0";
            }

        }

        private void FilterBy(DataTable DataTable, string ColumnName)
        {
            DataTable filteredTable = DataTable.Clone();

            foreach (DataRow row in DataTable.Rows)
            {
                string value = row[ColumnName].ToString();

                if (value.ToUpper().Contains(txtFilterBy.Text.ToUpper()))
                {
                    filteredTable.ImportRow(row);

                    //break;
                }
                else
                {
                    dgvAllPeople.DataSource = null;

                    lblRecodes.Text = "0";
                }

            }


            dgvAllPeople.DataSource = filteredTable;
            //_AdjustSizeDGV();
            lblRecodes.Text = filteredTable.Rows.Count.ToString();


        }




        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllPeople.CurrentRow.Cells[0].Value;

            frmShowDetailsPerson frmShowDetailsPerson = new frmShowDetailsPerson(ID);
            frmShowDetailsPerson.ShowDialog();
        }
    }
}
