using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Manage_Drivers
{
    public partial class frmListDrivers : Form
    {
        public frmListDrivers()
        {
            InitializeComponent();
        }


        private void _RefreshDriversList()
        {
            DataTable DriverDataTable = clsDriver.GetAllDriver_1();
            dgvAllDrivers.DataSource = DriverDataTable;

            _AdjustSizeDGV();

            lblRecodes.Text = DriverDataTable.Rows.Count.ToString();

        }

        private void _AdjustSizeDGV()
        {
            if (dgvAllDrivers.Columns.Count <= 0)
                return;

            dgvAllDrivers.Columns[0].Width = 30;
            dgvAllDrivers.Columns[1].Width = 30;
            dgvAllDrivers.Columns[2].Width = 30;
            dgvAllDrivers.Columns[3].Width = 80;
            dgvAllDrivers.Columns[4].Width = 40;
            dgvAllDrivers.Columns[5].Width = 250;
            

        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _RefreshDriversList();
            cbFilterBy.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public enum enFilterBy { None, DriverID, PersonID, NationalNo, FullName }
        
        private enFilterBy _enFilterBy = enFilterBy.None;

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _enFilterBy = (enFilterBy)cbFilterBy.SelectedIndex;

            if (txtFilterBy.Text != "")
                txtFilterBy.Text = string.Empty;

            if (_enFilterBy == enFilterBy.None)
            {
                txtFilterBy.Visible = false;

                _RefreshDriversList();
            }
            else
            {
                txtFilterBy.Visible = true;
                txtFilterBy.Focus();
            }

        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = clsDriver.GetAllDriver_1();
            DataRow[] ResultRows = new DataRow[0];

            if (txtFilterBy.Text != "")
            {
                switch (_enFilterBy)
                {
                    case enFilterBy.DriverID:
                        {
                            FilterBy(dt, ResultRows, $"[DriverID] = {txtFilterBy.Text} ");

                            break;
                        }
                    case enFilterBy.PersonID:
                        {
                            FilterBy(dt, ResultRows, $"[PersonID] = {txtFilterBy.Text} ");

                            break;
                        }
                    case enFilterBy.NationalNo:
                        {

                            FilterBy(dt, ResultRows, $"[NationalNo] = '{txtFilterBy.Text}' ");

                            break;
                        }
                    case enFilterBy.FullName:
                        {
                            FilterBy(dt, "FullName");

                            break;
                        }

                }



            }

        }


        private void FilterBy(DataTable DataTable, DataRow[] ResultRows, string Select)
        {

            ResultRows = DataTable.Select(Select);

            if (ResultRows.Length > 0)
            {
                dgvAllDrivers.DataSource = ResultRows.CopyToDataTable();
                _AdjustSizeDGV();
                lblRecodes.Text = ResultRows.Count().ToString();

            }
            else
            {
                dgvAllDrivers.DataSource = null;

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
                    dgvAllDrivers.DataSource = null;

                    lblRecodes.Text = "0";
                }

            }


            dgvAllDrivers.DataSource = filteredTable;
            _AdjustSizeDGV();
            lblRecodes.Text = filteredTable.Rows.Count.ToString();


        }





    }
}
