using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FastSync1
{
    public partial class approveevent : Form
    {
        string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";
        //     conn_string = "Data Source=ASIM-SHARIF\\SQLEXPRESS;Initial Catalog=FastSync;Integrated Security=True";
        private DataTable eventRegistrationTable;

        public approveevent()
        {
            InitializeComponent();
            
        }

        private void PopulateEventRegistrationRequests()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT EventName, EventDate, EventTime, Organizer, RequestDate, ApprovalStatus,description 
                             FROM EventRegistrationRequest";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == approvecolumn.Index)
            {
                string requestId = (dataGridView1.Rows[e.RowIndex].Cells["EventName"].Value).ToString();
                ApproveRequest(requestId);
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == rejectcolumn.Index)
            {
                string requestId = (dataGridView1.Rows[e.RowIndex].Cells["EventName"].Value).ToString();
                RejectRequest(requestId);
            }
        }


        private void ApproveRequest(string EventName)
        {
            Console.WriteLine("asdsad");
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Fetch details of the approved request
                    string getRequestQuery = "SELECT EventName, EventDate, EventTime, Organizer, description FROM EventRegistrationRequest WHERE EventName = @EventName";
                    SqlCommand getRequestCmd = new SqlCommand(getRequestQuery, conn);
                    getRequestCmd.Parameters.AddWithValue("@EventName", EventName);

                    SqlDataReader reader = getRequestCmd.ExecuteReader();
                    string eventName = "";
                    DateTime eventDate = DateTime.Now;
                    TimeSpan eventTime = TimeSpan.Zero;
                    string organizer = "";
                    string description = "";

                    Console.WriteLine("A1");
                    if (reader.Read())
                    {
                        eventName = reader["EventName"].ToString();
                        eventDate = Convert.ToDateTime(reader["EventDate"]);
                        eventTime = TimeSpan.Parse(reader["EventTime"].ToString());
                        organizer = reader["Organizer"].ToString();
                        if (reader["description"] != DBNull.Value) 
                        {
                            description = reader["description"].ToString();
                        }
                    }
                    reader.Close();
                    Console.WriteLine("A2");
                    // Get the last event_id from the Events table and increment by 1
                    int newEventId = 1; // Default value if the table is empty
                    string getLastEventIdQuery = "SELECT MAX(event_id) FROM Events";
                    SqlCommand getLastEventIdCmd = new SqlCommand(getLastEventIdQuery, conn);
                    newEventId = (int)getLastEventIdCmd.ExecuteScalar();
                    newEventId += 1;
                    Console.WriteLine("A3");
                    // Insert the event into the Events table with the calculated event_id
                    string insertEventQuery = "INSERT INTO Events (event_id, name, date, time, society_id,description) VALUES (@EventId, @EventName, @EventDate, @EventTime, (SELECT society_id FROM Societies WHERE name = @Organizer),@description)";
                    SqlCommand insertEventCmd = new SqlCommand(insertEventQuery, conn);
                    insertEventCmd.Parameters.AddWithValue("@EventId", newEventId);
                    insertEventCmd.Parameters.AddWithValue("@EventName", eventName);
                    insertEventCmd.Parameters.AddWithValue("@EventDate", eventDate);
                    insertEventCmd.Parameters.AddWithValue("@EventTime", eventTime);
                    insertEventCmd.Parameters.AddWithValue("@Organizer", organizer);
                    insertEventCmd.Parameters.AddWithValue("@description",description);
                    int rowsAffected = insertEventCmd.ExecuteNonQuery();

                    // If insertion into Events table is successful, delete the request
                    if (rowsAffected > 0)
                    {
                        string deleteRequestQuery = "DELETE FROM EventRegistrationRequest WHERE EventName = @EventName";
                        SqlCommand deleteRequestCmd = new SqlCommand(deleteRequestQuery, conn);
                        deleteRequestCmd.Parameters.AddWithValue("@EventName", EventName);
                        int deleteRowsAffected = deleteRequestCmd.ExecuteNonQuery();

                        if (deleteRowsAffected > 0)
                        {
                            MessageBox.Show("Request approved successfully. Event added to the Events table.");
                            PopulateEventRegistrationRequests();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete request.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to add event.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void RejectRequest(string eventName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Find the request ID based on the event name
                    string requestIdQuery = "SELECT RequestID FROM EventRegistrationRequest WHERE EventName = @EventName";
                    SqlCommand requestIdCmd = new SqlCommand(requestIdQuery, conn);
                    requestIdCmd.Parameters.AddWithValue("@EventName", eventName);
                    object requestIdObj = requestIdCmd.ExecuteScalar();

                    if (requestIdObj != null && requestIdObj != DBNull.Value)
                    {
                        int requestId = Convert.ToInt32(requestIdObj);

                        // Update the approval status to "Rejected" in the database
                        UpdateApprovalStatus(requestId, "Rejected");

                        // Update the ApprovalStatus cell in the DataGridView
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["EventName"].Value != null && row.Cells["EventName"].Value.ToString() == eventName)
                            {
                                row.Cells["ApprovalStatus"].Value = "Rejected";
                                break;
                            }
                        }

                        MessageBox.Show("Request has been successfully rejected.", "Rejection Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No request found for the specified event name.", "Request Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateApprovalStatus(int requestId, string status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE EventRegistrationRequest SET ApprovalStatus = @Status WHERE RequestID = @RequestID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminHomePage adminPage = new AdminHomePage();
            adminPage.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PopulateEventRegistrationRequests();
        }
    }
}
