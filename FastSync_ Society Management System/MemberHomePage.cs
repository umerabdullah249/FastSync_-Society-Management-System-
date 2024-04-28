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
    public partial class MemberHomePage : Form
    {


        private int memberId;

        public MemberHomePage()
        {
            InitializeComponent();
        }

        public MemberHomePage(int memberId)
        {
            InitializeComponent();
            this.memberId = memberId;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ViewSocieties vwSc = new ViewSocieties();
            vwSc.Show();
            //this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewEvents vwEv = new ViewEvents();
            vwEv.Show();
            // this.Hide();
        }
        //private void button3_Click(object sender, EventArgs e)
        //{
        //  Tasks tks = new Tasks(memberId);
        //    tks.Show();
        //   // this.Hide();
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Tasks tks = new Tasks(memberId);
            tks.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FeedbackForm abc = new FeedbackForm();
            abc.Show();
            this.Hide();
        }
    }
}
