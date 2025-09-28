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

        public frmShowDetailsPerson(int PersonID)
        {
            InitializeComponent();
            ucPersonDetails1.LoadPersonInfo(PersonID);
        }

        public frmShowDetailsPerson(string NationalNo)
        {
            InitializeComponent();
            ucPersonDetails1.LoadPersonInfo(NationalNo);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
