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
    public partial class ApproveSoc : Form
    {
        private string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";

        public ApproveSoc()
        {
            InitializeComponent();
            PopulateSocietyRegistrationRequests();
        }

        private void PopulateSocietyRegistrationRequests()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT SocietyName, SocietyDescription, RequestedBy, RequestDate, ApprovalStatus 
                                     FROM SocietyRegistrationRequest";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void ApproveRequest(string societyName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getRequestQuery = "SELECT SocietyName, SocietyDescription,RequestDate, RequestedBy, ApprovalStatus FROM SocietyRegistrationRequest WHERE SocietyName = @SocietyName";
                    SqlCommand getRequestCmd = new SqlCommand(getRequestQuery, conn);
                    getRequestCmd.Parameters.AddWithValue("@SocietyName", societyName);

                    SqlDataReader reader = getRequestCmd.ExecuteReader();
                    string mentor = "";
                    string societyDescription = "";

                    if (reader.Read())
                    {
                        mentor = reader["RequestedBy"].ToString();
                        societyDescription = reader["SocietyDescription"].ToString();
                    }
                    reader.Close();

                    int societyId = GetNextSocietyId(conn);

                    string insertSocietyQuery = "INSERT INTO Societies (society_id, name, mentor, description) VALUES (@SocietyId, @SocietyName, @Mentor, @Description)";
                    SqlCommand insertSocietyCmd = new SqlCommand(insertSocietyQuery, conn);
                    insertSocietyCmd.Parameters.AddWithValue("@SocietyId", societyId);
                    insertSocietyCmd.Parameters.AddWithValue("@SocietyName", societyName);
                    insertSocietyCmd.Parameters.AddWithValue("@Mentor", mentor);
                    insertSocietyCmd.Parameters.AddWithValue("@Description", societyDescription);
                    insertSocietyCmd.ExecuteNonQuery();

                    UpdateApprovalStatus(conn, societyName, "Approved");

                    MessageBox.Show("Request approved successfully. Society added to the Societies table.");
                    PopulateSocietyRegistrationRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void RejectRequest(string societyName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    UpdateApprovalStatus(conn, societyName, "Rejected");

                    MessageBox.Show("Request has been successfully rejected.", "Rejection Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateSocietyRegistrationRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateApprovalStatus(SqlConnection conn, string societyName, string status)
        {
            try
            {
                string query = "UPDATE SocietyRegistrationRequest SET ApprovalStatus = @Status WHERE SocietyName = @SocietyName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@SocietyName", societyName);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (status == "Approved" && rowsAffected > 0)
                    {
                        DeleteRequest(conn, societyName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteRequest(SqlConnection conn, string societyName)
        {
            try
            {
                string deleteQuery = "DELETE FROM SocietyRegistrationRequest WHERE SocietyName = @SocietyName";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@SocietyName", societyName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetNextSocietyId(SqlConnection conn)
        {
            string query = "SELECT ISNULL(MAX(society_id), 0) + 1 FROM Societies";
            SqlCommand cmd = new SqlCommand(query, conn);
            int nextId = Convert.ToInt32(cmd.ExecuteScalar());
            return nextId;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == approvecolumn.Index)
            {
                string societyName = dataGridView1.Rows[e.RowIndex].Cells["SocietyName"].Value.ToString();
                ApproveRequest(societyName);
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == rejectcolumn.Index)
            {
                string societyName = dataGridView1.Rows[e.RowIndex].Cells["SocietyName"].Value.ToString();
                RejectRequest(societyName);
            }
        }

        private void cancelbuttonbutton1_Click(object sender, EventArgs e)
        {
            AdminHomePage adminPage = new AdminHomePage();
            adminPage.Show();
            this.Close();
        }
    }
}