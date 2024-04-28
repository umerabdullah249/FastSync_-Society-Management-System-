using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace FastSync1
{
    public partial class Login : Form
    {
        string conn_string;

        public Login()
        {
            InitializeComponent();
            conn_string = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Validate user ID
            if (!IsValidUserID(username))
            {
                label4.Text = "Invalid user ID.";
                return;
            }

            // Validate password
            if (!IsValidPassword(password))
            {
                label4.Text = "Invalid password.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(conn_string))
            {
                conn.Open();
                string query = "SELECT user_id, user_type FROM Users WHERE user_id = @Username AND PASSWORD = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        MessageBox.Show("Logged in!!");
                        // User authentication successful
                        int userId = Convert.ToInt32(reader["user_id"]);
                        string userType = reader["user_type"].ToString();

                        if (userType == "faculty")
                        {
                            facultyhomepage fachome = new facultyhomepage(userId);
                            fachome.Show();
                            this.Hide();
                        }
                        else if (userType == "president")
                        {
                            presidentHomepage preshome = new presidentHomepage();
                            preshome.Show();
                            this.Hide();
                        }
                        else if (userType == "member")
                        {
                            // Open the Tasks form passing the user ID
                            MemberHomePage mhp = new MemberHomePage(userId);
                            mhp.Show();
                            this.Hide();
                        }
                        else
                        {
                            AdminHomePage adminPage = new AdminHomePage();
                            adminPage.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        label4.Text = "Incorrect username or password.";
                    }
                }
            }
        }

        private bool IsValidUserID(string userId)
        {
            // Check if user ID is numeric
            if (!int.TryParse(userId, out _))
                return false;

            // Check length constraint
            if (userId.Length > 10 || userId.Length < 1)
                return false;

            // Check if it starts with zero
            if (userId.StartsWith("0"))
                return false;

            return true;
        }

        private bool IsValidPassword(string password)
        {
            // Check length constraint
            if (password.Length < 5 || password.Length > 20)
                return false;

            // Check if password contains any spaces
            if (password.Contains(" "))
                return false;

            return true;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private string GetMentorName(int userId)
        {
            string username = string.Empty;

            using (SqlConnection connection = new SqlConnection(conn_string))
            {
                connection.Open();
                string query = "SELECT user_id, user_type, member_id FROM Users WHERE username = @Username AND PASSWORD = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    username = command.ExecuteScalar()?.ToString();
                }
            }

            return username;
        }
    }
}