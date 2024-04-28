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


namespace FastSync1
{
    public partial class RegisterEvent : Form
    {
        private const string connectionString = "Data Source=DESKTOP-K7PI5QI\\MSSQLSERVER02;Initial Catalog=FastSync;Integrated Security=True";

        public RegisterEvent()
        {
            InitializeComponent();
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            // Navigate back to the faculty homepage
            presidentHomepage facultyHomePage = new presidentHomepage();
            facultyHomePage.Show();
            this.Hide();
        }
        private static readonly int[] Y1 = { 1900 };
        private static readonly int[] Y2 = Enumerable.Range(1812, 2020 - 1812 + 1)
                                                    .Where(year => year != 1900 && year % 4 == 0)
                                                    .ToArray();
        private static readonly int[] Y3 = Enumerable.Range(1812, 2020 - 1812 + 1)
                                                    .Where(year => year % 4 != 0)
                                                    .ToArray();
        private static readonly int[] M1 = { 1, 3, 5, 7, 8, 10, 12 };
        private static readonly int[] M2 = { 4, 6, 9, 11 };
        private static readonly int[] M3 = { 2 };
        private static readonly int[] D1 = Enumerable.Range(1, 28).ToArray();
        private static readonly int[] D2 = { 29 };
        private static readonly int[] D3 = { 30 };
        private static readonly int[] D4 = { 31 };

        private void registerbtn_Click(object sender, EventArgs e)
        {
            string eventName = eventNameTxtBox.Text;
            string organizer = organizerTxtBox.Text;
            string eventDateStr = eventDateTxtBox.Text;
            string eventTimeStr = eventTimeTxtBox.Text;

            // Validate event date and time
            if (!DateTime.TryParse(eventDateStr, out DateTime eventDate))
            {
                MessageBox.Show("Please enter a valid event date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!TimeSpan.TryParse(eventTimeStr, out TimeSpan eventTime))
            {
                MessageBox.Show("Please enter a valid event time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate date constraints
            int month = eventDate.Month;
            int day = eventDate.Day;
            int year = eventDate.Year;

            bool isValidMonth = false;
            bool isValidDay = false;
            bool isValidYear = false;

            if ((month >= 1 && month <= 12) &&
                ((M1.Contains(month) && D1.Contains(day)) ||
                 (M2.Contains(month) && D2.Contains(day)) ||
                 (M2.Contains(month) && D3.Contains(day)) ||
                 (M3.Contains(month) && D4.Contains(day))))
            {
                isValidMonth = true;
                isValidDay = true;
            }

            if ((Y1.Contains(year)) ||
                (Y2.Contains(year) && (year != 1900) && (year % 4 == 0)) ||
                (Y3.Contains(year) && (year % 4 != 0)))
            {
                isValidYear = true;
            }

            if (!isValidMonth || !isValidDay || !isValidYear)
            {
                MessageBox.Show("Please enter a valid event date within the specified constraints.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Insert the event registration request
            InsertEvent(eventName, organizer, eventDate, eventTime);
        }


        private void InsertEvent(string eventName, string organizer, DateTime eventDate, TimeSpan eventTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert the event registration request into the table
                    string query = "INSERT INTO EventRegistrationRequest (EventName, EventDate, EventTime, Organizer, RequestDate, ApprovalStatus) " +
                                   "VALUES (@EventName, @EventDate, @EventTime, @Organizer, GETDATE(), 'Pending')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EventName", eventName);
                        command.Parameters.AddWithValue("@EventDate", eventDate);
                        command.Parameters.AddWithValue("@EventTime", eventTime);
                        command.Parameters.AddWithValue("@Organizer", organizer);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Event registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            eventNameTxtBox.Clear();
            organizerTxtBox.Clear();
            eventDateTxtBox.Clear();
            eventTimeTxtBox.Clear();
        }
    }
}
