using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FastSync1
{
    public partial class FeedbackForm : Form
    {
        string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";

        public FeedbackForm()
        {
            InitializeComponent();
        }

        private void FeedbackForm_Load(object sender, EventArgs e)
        {
            LoadPastEvents();
        }

        private void LoadPastEvents()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT name AS EventName, date AS Date, society_id AS SocietyID FROM PastEvents";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // If the user clicks on the first column (event name)
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                string selectedEventName = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string eventDetails = GetEventDetails(selectedEventName);
                ShowFeedbackForm(selectedEventName, eventDetails);
            }
        }

        private string GetEventDetails(string eventName)
        {
            string eventDetails = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT S.name AS SocietyName
                    FROM PastEvents PE
                    INNER JOIN Societies S ON PE.society_id = S.society_id
                    WHERE PE.name = @EventName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EventName", eventName);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            string societyName = reader["SocietyName"].ToString();
                            eventDetails = societyName;
                        }
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return eventDetails;
        }

        private void ShowFeedbackForm(string eventName, string details)
        {
            FeedbackForm1 feedbackForm = new FeedbackForm1(eventName, details);
            feedbackForm.Show();
            // this.Hide(); // Depending on your requirements, you may want to hide the current form or not
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPastEvents();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Please enter an event name to search.");
                return;
            }
            SearchAndDisplayEventDetails(searchText);
        }

        private void SearchAndDisplayEventDetails(string eventName)
        {
            string details = GetEventDetails(eventName);
            if (string.IsNullOrEmpty(details))
            {
                MessageBox.Show("Event not found.");
            }
            else
            {
                ShowFeedbackForm(eventName, details);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MemberHomePage adsf = new MemberHomePage();
            adsf.Show();
            this.Close();
        }
    }
}
