using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPGGAME2
{
    public partial class rpggame1 : Form
    {
        public rpggame1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            lblHitPoints.Text = "100";
            lblGold.Text = "100";
            lblLevel.Text = "1";
            lblExperience.Text = "0";
            btnStart.Hide();
        }

        private void lblExperience_Click(object sender, EventArgs e)
        {

        }

        private void lblHitPoints_Click(object sender, EventArgs e)
        {

        }

        private void lblEquipment_Click(object sender, EventArgs e)
        {

        }
    }
}
