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
        private DataTable _dtAllTestTypes;

        public frmManageTestTypes()
        {
            InitializeComponent();
           
        }

        private void _RefreshTestTypesList()
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes_1();
            dgvAllTestTypes.DataSource = _dtAllTestTypes;

            if (dgvAllTestTypes.Rows.Count > 0)
            {
                dgvAllTestTypes.Columns["ID"].Width = 30;
                dgvAllTestTypes.Columns["Title"].Width = 70;
                dgvAllTestTypes.Columns["Description"].Width = 200;
                dgvAllTestTypes.Columns["Fees"].Width = 100;
            }

            lblRecodes.Text = dgvAllTestTypes.Rows.Count.ToString();

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
            clsTestType.enTestType TestTypeID = (clsTestType.enTestType)dgvAllTestTypes.CurrentRow.Cells[0].Value;

            frmUpdateTestType frm = new frmUpdateTestType(TestTypeID);
            frm.ShowDialog();

            _RefreshTestTypesList();
        }
    }
}
