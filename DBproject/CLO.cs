using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;

namespace DBproject
{
    public partial class CLO : Form
    {
        private string ConnectionString = @"Data Source=DESKTOP-13N6TJM;Initial Catalog=ProjectB;Integrated Security=True;";
        int selectedCloId;
        public CLO()
        {
            InitializeComponent();
            DisplayClo();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void Stubtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Student student = new Student();
            student.Show();
        }

        private void Rubricbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageAssesment manage = new ManageAssesment();
            manage.Show();
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Nametxt.Text == "")
                {
                    MessageBox.Show("missing data!!");
                }

                string name = Nametxt.Text;
                SqlConnection connection = new SqlConnection(ConnectionString);

                connection.Open();
                string query = "INSERT INTO Clo (Name, DateCreated, DateUpdated) VALUES (@Name, GETDATE(), GETDATE())";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Clo added successfully.");
                    Nametxt.Clear();
                    DisplayClo();
                }
                else
                {
                    MessageBox.Show("Failed to add Clo.");
                }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedCloId = Convert.ToInt32(row.Cells[0].Value);
                Nametxt.Text = row.Cells["Name"].Value.ToString();

            }
        }
        private void DisplayClo()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Clo";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Clo");

                    if (dataSet.Tables.Count > 0)
                    {
                        dataGridView1.DataSource = dataSet.Tables["Clo"];
                    }
                    else
                    {
                        MessageBox.Show("No data found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        
        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCloId == 0)
                {
                    MessageBox.Show("No Clo selected.");
                    return;
                }

                if (Nametxt.Text == "")
                {
                    MessageBox.Show("Missing data!!");
                    return;
                }

                string name = Nametxt.Text;
                SqlConnection connection = new SqlConnection(ConnectionString);
                
                    connection.Open();
                    string query = "UPDATE Clo SET Name = @Name, DateUpdated = GETDATE() WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Id", selectedCloId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Clo updated successfully.");
                        Nametxt.Clear();
                        DisplayClo();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update Clo.");
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCloId == 0)
                {
                    MessageBox.Show("No Clo selected.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Clo WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@Id", selectedCloId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Clo deleted successfully.");
                        Nametxt.Clear();
                        DisplayClo();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete Clo.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
