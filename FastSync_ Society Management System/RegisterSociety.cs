using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastSync1
{
    public partial class RegisterSociety : Form
    {

        private const string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";

        private int mentorId;
        public RegisterSociety(int mentorId, string mentorName)
        {
            InitializeComponent();
            this.mentorId = mentorId;
            societymentortxtbox.Text = mentorName; // Set mentor's name in the text box
        }

        public RegisterSociety()
        {
            InitializeComponent();
        }

        private void societynametxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void societymentortxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void societypresidenttxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void societydescriptiontxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void backbtn_Click(object sender, EventArgs e)



        {      

            facultyhomepage fc = new facultyhomepage();
            fc.Show();
            this.Hide();
        }

        private void registerbtn_Click(object sender, EventArgs e)
        {
            string SName = societynametxtbox.Text;
            string Smentor = societymentortxtbox.Text;
            string Sdescription = societydescriptiontxtbox.Text;

            if (IsSocietyNameExists(SName))
            {
                MessageBox.Show("Society with the same name already exists. Please choose a different name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                InsertSociety(SName, Smentor, Sdescription);

                MessageBox.Show("Society registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearForm();


        }



        private bool IsSocietyNameExists(string societyName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Societies WHERE name = @SocietyName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SocietyName", societyName);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }


        private void InsertSociety(string SName, string Smentor, string Sdescription)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert the society registration request into the table
                    string query = "INSERT INTO SocietyRegistrationRequest (SocietyName, SocietyDescription, RequestedBy, RequestDate, ApprovalStatus) " +
                                   "VALUES (@Name, @Description, @RequestedBy, GETDATE(), 'Pending')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", SName);
                        command.Parameters.AddWithValue("@Description", Sdescription);
                        command.Parameters.AddWithValue("@RequestedBy", Smentor);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearForm()
        {
            societynametxtbox.Clear();
            societymentortxtbox.Clear();
            societydescriptiontxtbox.Clear();
        }
    }
}
