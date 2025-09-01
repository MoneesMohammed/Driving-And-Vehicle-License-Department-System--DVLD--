using DVLD_BusinessLayer;
using DVLD_DataAccessLayar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Test_Types
{
    public partial class frmManageTestTypes : Form
    {
        

        public frmManageTestTypes()
        {
            InitializeComponent();
           
        }

        private void _RefreshTestTypesList()
        {
            DataTable TestTypesDataTable = clsTestType.GetAllTestTypes_1();
            dgvAllTestTypes.DataSource = TestTypesDataTable;

            dgvAllTestTypes.Columns["ID"].Width = 30;
            dgvAllTestTypes.Columns["Title"].Width = 80;
            dgvAllTestTypes.Columns["Description"].Width = 150;
            dgvAllTestTypes.Columns["Fees"].Width = 100;

            lblRecodes.Text = TestTypesDataTable.Rows.Count.ToString();

        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllTestTypes.CurrentRow.Cells[0].Value;

            frmUpdateTestType updateTestType = new frmUpdateTestType(ID);
            updateTestType.ShowDialog();

            _RefreshTestTypesList();
        }
    }
}
