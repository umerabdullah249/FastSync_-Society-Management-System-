using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FastSync1
{
    public partial class ViewEvents : Form
    {
        string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";

        public ViewEvents()
        {
            InitializeComponent();
        }

        private void ViewEvents_Load(object sender, EventArgs e)
        {
            RefreshEvents();
        }

        private void RefreshEvents()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT name AS EventName, date AS Date, event_id AS EventID FROM Events";
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

        private void MovePastEvents()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Events WHERE date < @CurrentDate";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CurrentDate", DateTime.Today);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            AddToPastEvents(reader);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AddToPastEvents(SqlDataReader reader)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO PastEvents (name, date, society_id) VALUES (@EventName, @EventDate, @SocietyID)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EventName", reader["name"]);
                        command.Parameters.AddWithValue("@EventDate", reader["date"]);
                        command.Parameters.AddWithValue("@SocietyID", reader["society_id"]);
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

        private void DeletePastEvents()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Events WHERE date < @CurrentDate";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CurrentDate", DateTime.Today);
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

        private void button1_Click(object sender, EventArgs e)
        {
            MovePastEvents();
            DeletePastEvents();
            RefreshEvents();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                string selectedEventName = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string eventDetails = GetEventDetails(selectedEventName);
                ShowEventDetailsForm(selectedEventName, eventDetails);
            }
        }

        private string GetEventDetails(string eventName)
        {
            string eventDetails = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT date AS Date, event_id AS EventID FROM Events WHERE name = @EventName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EventName", eventName);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            eventDetails = $"Date: {reader["Date"]}, EventID: {reader["EventID"]}";
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

        private void ShowEventDetailsForm(string eventName, string details)
        {
            EventDetailsForm eventDetailsForm = new EventDetailsForm(eventName, details);
            eventDetailsForm.Show();
            this.Hide();
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
                ShowEventDetailsForm(eventName, details);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //presidentHomepage bk = new presidentHomepage();
            //bk.Show();
            this.Close();
        }
    }
}
