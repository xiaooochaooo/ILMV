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
    public partial class Form4 : Form
    {
        String ConString;
        SqlConnection conn;
        SqlCommand comm;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            ConString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C#\ILV\ILMV.mdf;Integrated Security=True;Connect Timeout=30";
            conn = new SqlConnection(ConString);
            comm = new SqlCommand();
            stuTableAdapter.Connection = conn;
            wordTableAdapter.Connection = conn;
            // TODO: 这行代码将数据加载到表“iLMVDataSet.Word”中。您可以根据需要移动或移除它。
            this.wordTableAdapter.Fill(this.iLMVDataSet.Word);
            // TODO: 这行代码将数据加载到表“iLMVDataSet.Stu”中。您可以根据需要移动或移除它。
            this.stuTableAdapter.Fill(this.iLMVDataSet.Stu);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num = wordTableAdapter.Update(iLMVDataSet);
            MessageBox.Show(num+"条数据已插入", "提示", MessageBoxButtons.OK);
        }
        public void getUsername(string text)
        {
            toolStripStatusLabel2.Text = text;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
