using System;
using System.Windows.Forms;

namespace FastSync1
{
    public partial class EventDetailsForm : Form
    {
        public EventDetailsForm(string eventName, string eventDescription)
        {
            InitializeComponent();

            // Set the labels or textboxes to display the eventName and eventDescription
            labelEventName.Text = eventName;
            textBoxDescription.Text = eventDescription;
        }

        private void EventDetailsForm_Load(object sender, EventArgs e)
        {
            // Any initialization code you want to run when the form loads
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Navigate back to the previous form (assuming it's called ViewEvents)
            ViewEvents viewEventsForm = new ViewEvents();
            viewEventsForm.Show();
            this.Hide();
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
