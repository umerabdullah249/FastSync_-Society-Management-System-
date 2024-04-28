using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FastSync1
{
    public partial class FeedbackForm1 : Form
    {
        private string SocietyName;
        private string EventName;
        private string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";

        public FeedbackForm1(string evNM, string societyNM)
        {
            EventName = evNM;
            SocietyName = societyNM;
            InitializeComponent();
        }

        private void FeedbackForm1_Load(object sender, EventArgs e)
        {
            label1.Text = "Event Name: " + EventName;
            label2.Text = "Society Name: " + SocietyName;
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubmitFeedback();
            }
        }

        private void SubmitFeedback()
        {
            string description = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(description))
            {
                AddFeedback(description);
                textBox1.Clear();
                MessageBox.Show("Feedback submitted successfully.");
            }
            else
            {
                MessageBox.Show("Please enter feedback before submitting.");
            }
        }

        private void AddFeedback(string description)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Feedback (eventName, societyName, description) VALUES (@EventName, @SocietyName, @Description)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EventName", EventName);
                        command.Parameters.AddWithValue("@SocietyName", SocietyName);

                        // Ensure description length doesn't exceed maximum length of the column
                        int maxLength = 1000; // Adjust as per your column's maximum length
                        if (description.Length > maxLength)
                        {
                            description = description.Substring(0, maxLength); // Truncate the description if it's too long
                        }

                        command.Parameters.AddWithValue("@Description", description);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void buttonSubmitFeedback_Click(object sender, EventArgs e)
        {
            SubmitFeedback();
        }
    }
}
