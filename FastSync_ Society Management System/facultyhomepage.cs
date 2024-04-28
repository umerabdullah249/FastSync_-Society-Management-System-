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
    public partial class facultyhomepage : Form
    {
        private int userid;
        public facultyhomepage()
        {
            InitializeComponent();
        }
        public facultyhomepage(int userid)
        {
            InitializeComponent();
            this.userid = userid;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RegisterSociety rs = new RegisterSociety();
            rs.Show();
            this.Hide();
        }

        private void facultyhomepage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login lg=new Login();
            lg.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewSocieties vwSc = new ViewSocieties();
            vwSc.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewEvents viewEvents = new ViewEvents();  
            viewEvents.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FeedbackForm abc = new FeedbackForm();
            abc.Show();
                this.Hide();
        }
    }
}
