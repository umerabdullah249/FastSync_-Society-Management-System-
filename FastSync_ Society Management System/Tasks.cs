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
    public partial class Tasks : Form
    {
        private int memberId;

        public Tasks(int memberId)
        {
            InitializeComponent();
            this.memberId = memberId;

            // Call a method to load tasks for the specific member
            LoadTasks();
        }

        private void LoadTasks()
        {
            // Connection string
            string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";

            // SQL query to select tasks for the specific member
            string query = "SELECT * FROM TaskAssignment WHERE member_id = @MemberId";

            // Create a new DataTable to hold the tasks
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        
                        connection.Open();
                        adapter.Fill(dataTable);

                        
                        connection.Close();
                    }
                }
            }

            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Hide();
        }
    


    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
