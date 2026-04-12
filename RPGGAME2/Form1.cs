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

        private Player _player = new Player();

        public rpggame1()
        {
            InitializeComponent();
            
            _player.MaxHp = 100;
            _player.CurrentHp = 100;
            _player.Gold = 100;
            _player.Level = 1;
            _player.Experience = 0;

            lblHitPoints.Text = _player.CurrentHp.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblLevel.Text = _player.Level.ToString();
            lblExperience.Text = _player.Experience.ToString();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
          
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
