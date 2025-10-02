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
        private DataTable _dtUsers = clsUser.GetAllUsers();

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
            _dtUsers = clsUser.GetAllUsers();

            dgvAllUsers.DataSource = _dtUsers;

            lblRecodes.Text = dgvAllUsers.Rows.Count.ToString();

        }

        private void butAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddNewUser frmAddNewUser = new frmAddNewUser();
            frmAddNewUser.ShowDialog();

            _RefreshUsersList();
        }

        private void tsmAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddNewUser frmAddNewUser = new frmAddNewUser();
            frmAddNewUser.ShowDialog();

            _RefreshUsersList();

        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;

            frmAddNewUser frmAddNewUser = new frmAddNewUser(UserID);
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


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (cbFilterBy.Text == "None")
            {
                txtFilterBy.Visible = false;
                cbIsActive.Visible  = false;
                _RefreshUsersList();
            }
            else if (cbFilterBy.Text == "Is Active")
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

            if (cbIsActive.Text == "All")
            {
                _RefreshUsersList();
                return;
            }


            string FilterColumn = (cbIsActive.Text == "Yes") ? "1" : "0";

            _dtUsers.DefaultView.RowFilter = string.Format($"[Is Active] = {FilterColumn}");

            lblRecodes.Text = dgvAllUsers.Rows.Count.ToString();
        }

      

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = cbFilterBy.Text;

            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None" || FilterColumn == "Is Active")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecodes.Text = _dtUsers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "Person ID" || FilterColumn == "User ID")
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim()); //[FilterColumn] = txtFilterBy.Text
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());
            //[FilterColumn] LIKE 'txtFilterBy.Text%'

            lblRecodes.Text = dgvAllUsers.Rows.Count.ToString();

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
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
            int UserID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;

            frmChangePassword frmChangePassword = new frmChangePassword(UserID);
            frmChangePassword.ShowDialog();


        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;
            
            var result = MessageBox.Show($"Are you sure you want to delete the User by UserID: {UserID}", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                if (clsUser.DeleteUser(UserID))
                {
                    
                    MessageBox.Show("User has been deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshUsersList();
                }
                else
                {
                    MessageBox.Show("User was not deleted because it has data linked to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;

            frmShowDetailsUser frm = new frmShowDetailsUser(UserID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
