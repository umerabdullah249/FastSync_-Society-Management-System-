using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using FastSync1;
namespace form1

{
    public partial class ViewSocieties : Form
    {
        string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";


        public ViewSocieties()
        {
            InitializeComponent();

            
        }

       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs f)
        {
            if (f.ColumnIndex == 0 && f.RowIndex != -1)
            {
                string selectedSocietyName = dataGridView1.Rows[f.RowIndex].Cells[f.ColumnIndex].Value.ToString();
                string description = GetSocietyDescription(selectedSocietyName);
                ShowDescriptionForm(selectedSocietyName, description);
            }
        }

        private string GetSocietyDescription(string societyName)
        {
            string description = string.Empty;
            try  //for error handling
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string query = "SELECT description AS Description FROM Societies WHERE name = @SocietyName";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@SocietyName", societyName);


                        connection.Open();


                        description = Convert.ToString(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return description;
        }



        private void ShowDescriptionForm(string socname, string des)
        {

            descriptionForm DescriptionForm = new descriptionForm(socname, des);
            DescriptionForm.ShowDialog(); // Show the form 
            this.Hide();    
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void societiesbutton_Click(object sender, EventArgs e)
        {

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQL query to select data from the database table
                    string query = "SELECT name AS Society,mentor AS Mentor,president_name AS President FROM Societies";

                    // Create a new SqlDataAdapter to fetch the data
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void searchbar_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            string searchText = searchbar.Text.Trim(); // Get the text entered in the search bar
            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Please enter a society name to search.");
                return;
            }
            SearchAndDisplaySocietyDescription(searchText);
        }

        private void SearchAndDisplaySocietyDescription(string societyName)
        {
            string description = GetSocietyDescription(societyName);
            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Society not found.");   //error checking
            }
            else
            {
                ShowDescriptionForm(societyName, description);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
                //presidentHomepage bk = new presidentHomepage();
                //bk.Show();
                this.Close();
           
        }
    }
}