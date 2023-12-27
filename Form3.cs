using System;
using System.Collections;
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
    public partial class Form3 : Form
    {
        String ConString;
        SqlConnection conn;
        SqlCommand comm;
        private int cnt;
        private int[] idx = new int[10];
        private List<string> words = new List<string>();
        private List<string> means = new List<string>();
        private int[] b = new int[10];
        int p = 0;
        string[] s = new string[4];
        int[] location = { 0, 1, 2, 3 };
        private int totalWord;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            cnt = 0;
            ConString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C#\ILV\ILMV.mdf;Integrated Security=True;Connect Timeout=30";
            conn = new SqlConnection(ConString);
            comm = new SqlCommand();
            stuTableAdapter.Connection = conn;
            wordTableAdapter.Connection = conn;
            // TODO: 这行代码将数据加载到表“iLMVDataSet.Word”中。您可以根据需要移动或移除它。
            this.wordTableAdapter.Fill(this.iLMVDataSet.Word);
            // TODO: 这行代码将数据加载到表“iLMVDataSet.Stu”中。您可以根据需要移动或移除它。
            this.stuTableAdapter.Fill(this.iLMVDataSet.Stu);
            string query = "select count(*) from word";
            conn.Open();
            comm = new SqlCommand(query, conn);
            totalWord= (int)comm.ExecuteScalar();
            conn.Close();
            random_10();
            foreach (int number in idx)
            {
                string sql = "select word,mean from word where id=" + number;
                conn.Open();
                comm = new SqlCommand(sql, conn);
                SqlDataReader Reader = comm.ExecuteReader();
                while (Reader.Read())
                {
                    words.Add(Reader[0].ToString());
                    means.Add(Reader[1].ToString());
                }
                Reader.Close();
                conn.Close();
            }
            update();
            
        }
        void update()
        {
            label2.Text = cnt + "/10";
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            label1.Text = words[p].Trim();
            //Fisher-Yates 洗牌算法
            location = new int[] { 0,1,2,3};
            Random rng = new Random();
            int n = location.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int temp = location[k];
                location[k] = location[n];
                location[n] = temp;
            }
            s = new string[4];
            s[0] = means[p];
            for (int i = 1; i < 4; i++)
            {
                int randomNumber;
                do
                {
                    randomNumber = rng.Next(1, totalWord);
                } while (randomNumber == idx[p]);
                String sql= "select mean from word where id=" + randomNumber;
                conn.Open();
                comm = new SqlCommand(sql, conn);
                s[i]= (string)comm.ExecuteScalar();
                conn.Close();
            }
            button1.Text = s[location[0]].Trim();
            button2.Text = s[location[1]].Trim();
            button3.Text = s[location[2]].Trim();
            button4.Text = s[location[3]].Trim();
            
        }
        void random_10()
        {
            HashSet <int> uniqueNumbers = new HashSet<int>();
            Random random = new Random();
            while (uniqueNumbers.Count < 10)
            {
                int randomNumber = random.Next(1, totalWord);
                uniqueNumbers.Add(randomNumber);
            }
            int i = 0;
            foreach (int number in uniqueNumbers)
            {
                idx[i] = number;
                i++;
            }
        }
        void chooseRight()
        {
            cnt++;
            b[p]++;
            if (cnt == 10)
            {
                label2.Text = "10/10";
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                String sql = "update Stu set cnt=cnt+10 where username='" + toolStripStatusLabel2.Text + "'"; 
                conn.Open();
                comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("已成功背完一组如要继续背点击操作再背一组", "提示", MessageBoxButtons.OK);
            }
            else
            {
                p = (p + 1) % 10;
                while (b[p] == 1)
                {
                    p = (p + 1) % 10;
                }
                update();
            }

        }
        void chooseWrong(int x)
        {
            if(button1.Enabled == true&&button2.Enabled ==true&&button3.Enabled == true&&button4.Enabled == true) cnt--;
            b[p] = -1;
            if (x == 1)
            {
                button1.Enabled = false;
            }
            else if (x == 2)
            {
                button2.Enabled = false;
            }
            else if (x == 3)
            {
                button3.Enabled = false;
            }
            else
            {               
                button4.Enabled = false;
            }
        }
        public void getUsername(string text)
        {
            toolStripStatusLabel2.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == means[p].Trim())
            {
                chooseRight();
            }
            else
            {
                chooseWrong(1);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == means[p].Trim())
            {
                chooseRight();
            }
            else
            {
                chooseWrong(2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == means[p].Trim())
            {
                chooseRight();
            }
            else
            {
                chooseWrong(3);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == means[p].Trim())
            {
                chooseRight();
            }
            else
            {
                chooseWrong(4);
            }
        }

        private void 保存并退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String sql = "update Stu set cnt=cnt+" + cnt + " where username='" + toolStripStatusLabel2.Text + "'";
            conn.Open();
            comm = new SqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            Close();
        }

        private void 查询自己背单词量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String sql = "select cnt from Stu where username='"+toolStripStatusLabel2 + "'";
            conn.Open();
            comm = new SqlCommand(sql, conn);
            int x=(int)comm.ExecuteScalar();
            conn.Close();
            MessageBox.Show("你已经背了"+x+"个单词", "提示", MessageBoxButtons.OK);
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String sql = "update Stu set cnt=cnt+" + cnt + " where username='" + toolStripStatusLabel2.Text + "'";
            conn.Open();
            comm = new SqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }

        private void 保存并退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cnt != 10)
            {
                保存ToolStripMenuItem_Click(sender, e);
            }
            Form3_Load(sender, e);
        }
    }
}
