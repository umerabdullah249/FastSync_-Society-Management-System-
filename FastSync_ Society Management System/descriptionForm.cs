using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form1
{
    public partial class descriptionForm : Form
    {
        public descriptionForm(string societyName, string description)
        {
            InitializeComponent();

            // Set the labels or textboxes to display the societyName and description
            labelSocietyName.Text = societyName;
            textBoxDescription.Text = description;


        }

        private void descriptionForm_Load(object sender, EventArgs e)
        {

        }

        private void backbuttondesc_Click(object sender, EventArgs e)
        {
            ViewSocieties form1 = new ViewSocieties();
            form1.Show();
            this.Hide();
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
