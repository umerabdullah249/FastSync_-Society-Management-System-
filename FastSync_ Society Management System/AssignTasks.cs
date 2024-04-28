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
    public partial class AssignTasks : Form
    {
        // Connection string
        string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";

        public AssignTasks()
        {
            InitializeComponent();
        }

        private void AssignTasks_Load(object sender, EventArgs e)
        {
            // Load data into dataGridView1
            LoadData();
        }

        private void LoadData()
        {
            // Create a new connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to select all data from TaskAssignment table
                string query = "SELECT task_id, member_id, username, status FROM TaskAssignment";

                // Create a SqlCommand object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Create a SqlDataAdapter
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // Create a DataTable to hold the results
                        DataTable dataTable = new DataTable();

                        // Open the connection
                        connection.Open();

                        // Fill the DataTable with data from the SQL query
                        adapter.Fill(dataTable);

                        // Close the connection
                        connection.Close();

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void UpdateStatus(string memberId, string newStatus)
        {
            // Create a new connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to update the status based on member ID
                string query = "UPDATE TaskAssignment SET status = @newStatus WHERE member_id = @memberId";

                // Create a SqlCommand object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SqlCommand
                    command.Parameters.AddWithValue("@newStatus", newStatus);
                    command.Parameters.AddWithValue("@memberId", memberId);

                    // Open the connection
                    connection.Open();

                    // Execute the query
                    command.ExecuteNonQuery();

                    // Close the connection
                    connection.Close();
                }
            }

            // Refresh dataGridView1 to reflect the changes
            LoadData();
        }

        // Event handler for button click to update status
        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            // Get the member ID and new status from the text boxes
            string memberId = textBox1.Text;
            string newStatus = textBox2.Text;

            // Call the UpdateStatus method to update the status
            UpdateStatus(memberId, newStatus);
        }
    





        ////private void AssignTasks_Load(object sender, EventArgs e)
        //{

        //}

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
