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

namespace MartSystem
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            cb_status.SelectedIndex = 0;
            LoadData();
        }


        private void btn_add_Click(object sender, EventArgs e)
        {
          SqlConnection conn = new SqlConnection("Data Source=DESKTOP-OR3LLG2\\SQLEXPRESS;Initial Catalog=MartSystem;Integrated Security=True");
            // Insert Login
            conn.Open();
            bool Status = false;
            if (cb_status.SelectedIndex == 0)
            {
                Status = true;
            }
            else {
                Status = false;
            }

            var sqlQuery="";
            if (IfProductsExits(conn,txt_pcode.Text))
            {

                sqlQuery = @"UPDATE Products SET ProductName='" + txt_pname.Text + "',ProductStatus='" + Status + "' WHERE ProductCode='" + txt_pcode.Text + "'";

            } else { 
                        
                sqlQuery= @"INSERT INTO Products  values ('"+txt_pcode.Text+"','"+txt_pname.Text +"','"+Status+"')";
         
            }
            SqlCommand cmd = new SqlCommand(sqlQuery,conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            //Reading Data
            LoadData();

        }

        private bool IfProductsExits(SqlConnection conn,string ProductCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT 1 FROM Products Where ProductCode='" + txt_pcode.Text + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;


        
        }

        public void LoadData() {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-OR3LLG2\\SQLEXPRESS;Initial Catalog=MartSystem;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Products", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {

                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {

                    dataGridView1.Rows[n].Cells[2].Value = "Active";

                }
                else
                {

                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }

            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txt_pcode.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txt_pname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() =="Active")
            {

                cb_status.SelectedIndex = 0;

            }
            else
            {

                cb_status.SelectedIndex = 1;
            }
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-OR3LLG2\\SQLEXPRESS;Initial Catalog=MartSystem;Integrated Security=True");
            var sqlQuery = "";
            if (IfProductsExits(conn, txt_pcode.Text))
            {
                conn.Open();
                sqlQuery = @"DELETE FROM Products WHERE ProductCode='" + txt_pcode.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
               

            }
            else
            {

                MessageBox.Show("The Recorde Not Exits..!");

            }
            //Reading Data
            LoadData();

        }

    }
}
