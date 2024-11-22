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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Final_Ioop.assa
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
           
        }
        string username;
        private SqlDataAdapter adap;
        private DataSet ds;


        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            timer1.Start();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {


            if (dataGridView1.CurrentRow != null && comboBox1.SelectedItem != null)

            {

                int requestId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["RequestID"].Value);
                string newStatus = comboBox1.SelectedItem.ToString();




                string connectionString = @"Server=DESKTOP-1CMM2KR\SQLEXPRESS01;Database=IOOP_Assignment;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(connectionString))




                {
                    con.Open();
                    string updateQuery = "UPDATE CustomerRequest SET RequestStatus = @NewStatus WHERE RequestID = @RequestID";
                    SqlCommand cmd = new SqlCommand(updateQuery, con);
                    cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmd.Parameters.AddWithValue("@RequestID", requestId);








                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        MessageBox.Show("Status updated successfully.");
                        Co_workerswStatues();
                        con.Close();






                    }

                    else
                    {
                        MessageBox.Show("Update failed. Please check the selection.");
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select a request to update the status.");


            }



        }
        private void Co_workerswStatues() 
        {
            string connstring = "Data Source=DESKTOP-1CMM2KR\\SQLEXPRESS01;Initial Catalog=IOOP_Assignment;Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                string query = "SELECT ";


                query += (comboBox3.SelectedItem == null || (comboBox3.SelectedItem != null && comboBox3.SelectedItem.ToString() != "New"))
                    ? "Worker_UserID, "
                    : "NULL AS Worker_UserID, ";

                query += "RequestID, RequestStatus FROM CustomerRequest WHERE 1=1";

                if (comboBox2.SelectedItem != null)
                {
                    string workerName = comboBox2.SelectedItem.ToString();
                    query += $" AND Worker_UserID = '{workerName}'";
                }

                if (comboBox3.SelectedItem != null)
                {
                    string selectedStatus = comboBox3.SelectedItem.ToString();
                    query += $" AND RequestStatus = '{selectedStatus}'";
                }

                SqlDataAdapter adap = new SqlDataAdapter(query, con);
                DataSet ds1 = new DataSet();
                adap.Fill(ds1, "FilteredRequests");
                dataGridView1.DataSource = ds1.Tables["FilteredRequests"];
            }













        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                Co_workerswStatues();

            }
            

     
    
        
        }









        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Refresh();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Co_workerswStatues();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Co_workerswStatues();
        }
    }
        
}
