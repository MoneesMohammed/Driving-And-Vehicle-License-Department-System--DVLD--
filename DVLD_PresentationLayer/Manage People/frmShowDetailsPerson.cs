using Driving___Vehicle_License_Department__DVLD_.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD_
{
    public partial class frmShowDetailsPerson : Form
    {

        private int _PersonID;

        public frmShowDetailsPerson(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void frmShowDetailsPerson_Load(object sender, EventArgs e)
        {
            ucPersonDetails1.LoadPersonInfo(_PersonID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
