using form1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastSync1
{
    public partial class AdminHomePage : Form
    {
        public AdminHomePage()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ViewEvents vwEv = new ViewEvents();
            vwEv.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewSocieties vwSc = new ViewSocieties();
            vwSc.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ApproveSoc approveSoc = new ApproveSoc();
            approveSoc.Visible = true;
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            approveevent approveeve = new approveevent();
            approveeve.Visible = true;
            this.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FeedbackForm abc = new FeedbackForm();
            abc.Show();
            this.Hide();
        }
    }
}
