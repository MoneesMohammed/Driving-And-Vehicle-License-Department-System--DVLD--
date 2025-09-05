using Driving___Vehicle_License_Department__DVLD_.Manage_Users;
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
using static Driving___Vehicle_License_Department__DVLD_.frmManageUsers;

namespace Driving___Vehicle_License_Department__DVLD_
{
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();

            cbFilterBy.SelectedIndex = 0;
        }


        private void _RefreshUsersList()
        {
            DataTable UsersDataTable = clsUser.GetAllUsers_1();
            dgvAllUsers.DataSource = UsersDataTable;

            lblRecodes.Text = UsersDataTable.Rows.Count.ToString();

        }

        private void butAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddNewUser frmAddNewUser = new frmAddNewUser(-1);
            frmAddNewUser.ShowDialog();

            _RefreshUsersList();
        }

        private void tsmAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddNewUser frmAddNewUser = new frmAddNewUser(-1);
            frmAddNewUser.ShowDialog();

            _RefreshUsersList();

        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;

            frmAddNewUser frmAddNewUser = new frmAddNewUser(ID);
            frmAddNewUser.ShowDialog();

            _RefreshUsersList();
        }


        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be available soon.", "Send Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsmPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be available soon.", "Phone Call", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public enum enIsActive { All,Yes,No}    
        private enIsActive _enIsActive = enIsActive.All;

        public enum enFilterBy { None,UserID,PersonID,UserName,FullName,IsActive } 
        private enFilterBy _enFilterBy = enFilterBy.None;


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            _enFilterBy = (enFilterBy)cbFilterBy.SelectedIndex;

            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (_enFilterBy == enFilterBy.None)
            {
                txtFilterBy.Visible = false;
                cbIsActive.Visible  = false;
                _RefreshUsersList();
            }
            else if (_enFilterBy == enFilterBy.IsActive)
            {
                cbIsActive.SelectedIndex = 0;

                txtFilterBy.Visible = false;
                cbIsActive.Visible = true;
            }
            else
            {
                txtFilterBy.Visible = true;
                cbIsActive.Visible  = false;
                txtFilterBy.Focus();
            }



        }


        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            _enIsActive = (enIsActive)cbIsActive.SelectedIndex;

            DataTable dt = clsUser.GetAllUsers_1();
            DataRow[] ResultRows = new DataRow[0];

            if (_enIsActive == enIsActive.Yes)
            {
                FilterBy(dt, ResultRows, "[Is Active] = 1");
            }
            else if (_enIsActive == enIsActive.No)
            {
                FilterBy(dt, ResultRows, "[Is Active] = 0");
            }
            else
            {
                _RefreshUsersList();
            }

        }

      

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_enFilterBy == enFilterBy.PersonID || _enFilterBy == enFilterBy.UserID)
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
            DataTable dt = clsUser.GetAllUsers_1();
            DataRow[] ResultRows = new DataRow[0];

            if (txtFilterBy.Text != "")
            {
                switch (_enFilterBy)
                {
                    case enFilterBy.UserID:
                        {
                            FilterBy(dt, ResultRows, $"[User ID] = {txtFilterBy.Text} ");

                            break;
                        }
                    case enFilterBy.PersonID:
                        {

                            FilterBy(dt, ResultRows, $"[Person ID] = {txtFilterBy.Text} ");

                            break;
                        }
                    case enFilterBy.UserName:
                        {
                            FilterBy(dt,"UserName");

                            break;
                        }
                    case enFilterBy.FullName:
                        {
                            FilterBy(dt, "Full Name");

                            break;
                        }



                }
            }


            

        }


        private void FilterBy(DataTable UsersDataTable, DataRow[] ResultRows, string Select)
        {
            
                ResultRows = UsersDataTable.Select(Select);

                if (ResultRows.Length > 0)
                {
                    dgvAllUsers.DataSource = ResultRows.CopyToDataTable();

                    lblRecodes.Text = ResultRows.Count().ToString();

                }
                else
                {
                    dgvAllUsers.DataSource = null;

                    lblRecodes.Text = "0";
                }
            

        }


        private void FilterBy(DataTable DataTable,string ColumnName)
        {
            DataTable filteredTable = DataTable.Clone();

            foreach (DataRow row in DataTable.Rows)
            {
                string value = row[ColumnName].ToString();

                if (value.ToUpper().Contains(txtFilterBy.Text.ToUpper()))
                {
                    filteredTable.ImportRow(row);

                    break;
                }
                else
                {
                    dgvAllUsers.DataSource = null;

                    lblRecodes.Text = "0";
                }

            }


            dgvAllUsers.DataSource = filteredTable;

            lblRecodes.Text = filteredTable.Rows.Count.ToString();


        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;

            frmChangePassword frmChangePassword = new frmChangePassword(ID);
            frmChangePassword.ShowDialog();


        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;
            

            var result = MessageBox.Show($"Are you sure you want to delete the User by UserID: {ID}", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                if (clsUser.DeleteUser(ID))
                {
                    
                    MessageBox.Show("User has been deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("User was not deleted because it has data linked to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }



            }

            _RefreshUsersList();

        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;

            frmShowDetailsUser frmShowDetailsUser = new frmShowDetailsUser(ID);
            frmShowDetailsUser.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
