using ILV.ILMVDataSet1TableAdapters;
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

namespace ILV
{
    public partial class Form2 : Form
    {
        String ConString;
        SqlConnection conn;
        SqlCommand comm;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 10)
            {
                textBox1.Text = "";
                MessageBox.Show("用户名过长", "提示", MessageBoxButtons.OK);
            }
            else if (textBox2.Text.Length > 20)
            {
                textBox2.Text = "";
                MessageBox.Show("密码过长", "提示", MessageBoxButtons.OK);
            }
            else if (textBox3.Text.Length > 20)
            {
                textBox3.Text = "";
                MessageBox.Show("姓名过长", "提示", MessageBoxButtons.OK);
            }
            else if((!radioButton1.Checked) && (!radioButton2.Checked))
            {
                MessageBox.Show("请选择性别", "提示", MessageBoxButtons.OK);
            }
            else
            {
                DataRow DR = iLMVDataSet.Tables["Login"].NewRow();
                DR["username"] = textBox1.Text;
                DR["password"] = textBox2.Text;
                DR["type"] = 1;
                iLMVDataSet.Tables["Login"].Rows.Add(DR);
                loginTableAdapter.Update(iLMVDataSet);
                DR = iLMVDataSet.Tables["Stu"].NewRow();
                DR["username"] = textBox1.Text;
                DR["name"] = textBox3.Text;
                if (radioButton1.Checked) DR["sex"] = "男";
                else DR["sex"] = "女";
                DR["cnt"] = 0;
                iLMVDataSet.Tables["Stu"].Rows.Add(DR);
                stuTableAdapter.Update(iLMVDataSet);
                MessageBox.Show("注册成功", "提示", MessageBoxButtons.OK);
                Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ConString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C#\ILV\ILMV.mdf;Integrated Security=True;Connect Timeout=30";
            conn = new SqlConnection(ConString);
            loginTableAdapter.Connection = conn;
            stuTableAdapter.Connection = conn;
            // TODO: 这行代码将数据加载到表“iLMVDataSet.Stu”中。您可以根据需要移动或移除它。
            this.stuTableAdapter.Fill(this.iLMVDataSet.Stu);
            // TODO: 这行代码将数据加载到表“iLMVDataSet.Login”中。您可以根据需要移动或移除它。
            this.loginTableAdapter.Fill(this.iLMVDataSet.Login);
            // TODO: 这行代码将数据加载到表“iLMVDataSet.Login”中。您可以根据需要移动或移除它。
        }
    }
}
