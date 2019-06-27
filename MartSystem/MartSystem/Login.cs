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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           txt_username.Text="";
            txt_password.Clear();
            txt_username.Focus();   
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-OR3LLG2\\SQLEXPRESS;Initial Catalog=MartSystem;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
                    	FROM Login Where UserName='"+ txt_username.Text +"' and Password='"+ txt_password.Text +"'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count == 1){
                this.Hide();
                 StockMain main = new StockMain();
                main.Show();

            }else{
            
            MessageBox.Show("Invailid UserNmae & Password..!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            button1_Click(sender,e);
            }

            
        }
    }
}
