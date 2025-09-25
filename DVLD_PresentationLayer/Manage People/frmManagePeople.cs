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
            DataTable PeopleDataTable = clsPerson.GetAllPeople_1();
            dgvAllPeople.DataSource = PeopleDataTable;

            lblRecodes.Text = PeopleDataTable.Rows.Count.ToString();



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
            int ID = (int)dgvAllPeople.CurrentRow.Cells[0].Value;

            string ImagePath = clsPerson.GetImagePath(ID);

            
          var result = MessageBox.Show($"Are you sure you want to delete the Person by PersonID: {ID}", "Warning", MessageBoxButtons.OKCancel ,MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(ID))
                {
                    if (ImagePath != "")
                        File.Delete(ImagePath);

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
            frmAddEditPersonInfo frmAddEditPersonInfo = new frmAddEditPersonInfo(-1);
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

       public enum enFilterBy {None,PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,Nationality,Gendor,Phone,Email }
       private     enFilterBy _enFilterBy = enFilterBy.None;


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _enFilterBy = (enFilterBy)cbFilterBy.SelectedIndex;

            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (_enFilterBy == enFilterBy.None)
            {
                txtFilterBy.Visible = false;
                _RefreshPeopleList();
            }
            else
            {
                txtFilterBy.Visible = true;
            }

            txtFilterBy.MaxLength = (_enFilterBy == enFilterBy.Phone) ? 10 : 32767;

        }
        

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_enFilterBy == enFilterBy.PersonID || _enFilterBy == enFilterBy.Phone)
            {
                //Code is  Allowing Only Numbers in txtFilterBy
                
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }


        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = clsPerson.GetAllPeople_1();
            DataRow[] ResultRows = new DataRow[0];

            switch (_enFilterBy)
            {
                case enFilterBy.PersonID :
                {
                        FilterBy(dt, ResultRows, $"PersonID = '{txtFilterBy.Text}' ");

                        break;
                }
                case enFilterBy.NationalNo:
                {

                        FilterBy(dt,ResultRows, $"[National No] = '{txtFilterBy.Text}' ");

                  break;
                }
                case enFilterBy.FirstName:
                {
                        FilterBy(dt, "First Name");

                        break;
                }
                case enFilterBy.SecondName:
                {

                        FilterBy(dt, "Second Name");

                        break;
                }
                case enFilterBy.ThirdName:
                {

                        FilterBy(dt,"Third Name");

                        break;
                }
                case enFilterBy.LastName:
                {
                        FilterBy(dt, "Last Name");

                        break;
                }
                case enFilterBy.Nationality:
                {

                        FilterBy(dt, "Nationality");

                        break;
                }
                case enFilterBy.Gendor:
                {

                        FilterBy(dt, ResultRows, $"Gendor = '{txtFilterBy.Text}' ");

                        if (txtFilterBy.Text.ToUpper() == "M")
                        {
                            FilterBy(dt, ResultRows, $"Gendor = 'Male' ");
                        }
                        else if (txtFilterBy.Text.ToUpper() == "F")
                        {
                            FilterBy(dt, ResultRows, $"Gendor = 'Female' ");

                        }

                            break;
                }
                case enFilterBy.Phone:
                {
                        
                        FilterBy(dt, "Phone");
                        break;
                }
                case enFilterBy.Email:
                {
                        FilterBy(dt, "Email");
                        

                        break;
                }



            }
                
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
