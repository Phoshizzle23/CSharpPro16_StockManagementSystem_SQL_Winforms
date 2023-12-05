using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StockManagementSystem
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-5KJ157N\\MATTDATABASEPRO;Initial Catalog=stockdb;User ID=sa;Password=1234qwer";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Add Button
        {
            if (!int.TryParse(textBox1.Text, out int productNumber))
            {
                MessageBox.Show("Invalid product number. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox3.Text, out int quantity))
            {
                MessageBox.Show("Invalid quantity. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!DateTime.TryParse(textBox4.Text, out DateTime date))
            {
                MessageBox.Show("Invalid date. Please enter a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO stocktab (productnumber, productname, price, quantity, date) VALUES (@productnumber, @productname, @price, @quantity, @date)", con);
                cmd.Parameters.AddWithValue("@ProductNumber", productNumber);
                cmd.Parameters.AddWithValue("@ProductName", textBox2.Text);
                cmd.Parameters.AddWithValue("@Price", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.ExecuteNonQuery();
            }

            BindData();

            MessageBox.Show("Record Added Successfully!!!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM stocktab", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void button2_Click(object sender, EventArgs e) // Cancel Button
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e) // Delete Button
        {
            if (!int.TryParse(textBox1.Text, out int productNumber))
            {
                MessageBox.Show("Invalid product number. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM stocktab WHERE productnumber=@Productnumber", con);
                cmd.Parameters.AddWithValue("@Productnumber", productNumber);
                cmd.ExecuteNonQuery();
            }

            BindData();

            MessageBox.Show("Stock Deleted");
        }

        private void button4_Click(object sender, EventArgs e) // Exit Button
        {
            this.Close();
            Environment.Exit(0);
        }
    }
}
