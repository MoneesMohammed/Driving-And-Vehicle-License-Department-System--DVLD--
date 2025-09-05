using Driving___Vehicle_License_Department__DVLD_.Applications.Manage_Application_Types;
using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_.Applications
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }


        private void _RefreshApplicationTypesList()
        {
            DataTable ApplicationTypesDataTable = clsApplicationType.GetAllApplicationTypes_1();
            dgvAllApplicationTypes.DataSource = ApplicationTypesDataTable;

            dgvAllApplicationTypes.Columns["ID"].Width = 50;
            dgvAllApplicationTypes.Columns["Title"].Width = 200;
            dgvAllApplicationTypes.Columns["Fees"].Width = 150;

            lblRecodes.Text = ApplicationTypesDataTable.Rows.Count.ToString();

        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllApplicationTypes.CurrentRow.Cells[0].Value;

            frmUpdateApplicationType frmUpdateApplicationType = new frmUpdateApplicationType(ID);
            frmUpdateApplicationType.ShowDialog();
            _RefreshApplicationTypesList();

        }
    }
}
