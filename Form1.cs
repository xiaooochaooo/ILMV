using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ILV
{
    public partial class Form1 : Form
    {
        String ConString;
        SqlConnection conn;
        SqlCommand comm;
        private Form2 form2;
        private Form3 form3;
        private Form4 form4;
        public Form1()
        {
            InitializeComponent();
            form2=new Form2();
            form3=new Form3(); 
            form4=new Form4(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C#\ILV\ILMV.mdf;Integrated Security=True;Connect Timeout=30";
            conn = new SqlConnection(ConString);
            comm = new SqlCommand();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("用户名为空", "提示", MessageBoxButtons.OK);
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("密码为空", "提示", MessageBoxButtons.OK);
            }
            else
            {
                string SQL = "Select * from Login where username='" + textBox1.Text + "'";
                conn.Open();
                comm.CommandText = SQL;
                comm.Connection = conn;
                SqlDataReader Reader = comm.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        String input_password = Reader[1].ToString().Trim();
                        int type = Convert.ToInt32(Reader[2]);
                        if (input_password.Equals(textBox2.Text))
                        {
                            if (type == 1)
                            {
                                form3.getUsername(textBox1.Text);
                                form3.Show();
                                //this.Visible = false;
                            }
                            else if (type == 2)
                            {
                                form4.getUsername(textBox1.Text);
                                form4.Show();
                                //this.Visible = false;
                            }
                        }
                        else
                        {
                            textBox1.Text = "";
                            textBox2.Text = "";
                            MessageBox.Show("用户名与密码不匹配", "提示", MessageBoxButtons.OK);
                        }
                    }
                    

                }
                else
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    MessageBox.Show("未找到该用户", "提示", MessageBoxButtons.OK);
                }
                Reader.Close();
                conn.Close();
            }
        }
    }
}
