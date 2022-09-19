using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace lotto
{
    public partial class Form1 : Form
    {
        int repeat;
        string text;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string conmStr = @"Data Source=C:\Users\Owner\Desktop\lottodb.db";
            string stm = "SELECT SQLITE_VERSION()";

            var con = new SQLiteConnection(conmStr);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine(version);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conmStr = @"Data Source=C:\Users\Owner\Documents\GitHub\lotto2\lottodb.db";
            var con = new SQLiteConnection(conmStr);
                con.Open();
            for(int i = 0; i <= repeat; i++)
            {
                string Number;
                Random random = new Random();
                int[] RandomArray = new int[6];
                for(int j = 0; j < 6; j++) {
                    RandomArray[j] = random.Next(1, 45);
                }
                Number = String.Join(",", RandomArray);
                String sql = "insert into LottoNumber (LottoNumber) values('" + Number + "')";
                text += Number + "\r\n";
                SQLiteCommand command = new SQLiteCommand(sql,con);
                Console.WriteLine(sql);

                int result = command.ExecuteNonQuery();
            }
            textBox1.Text = text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            repeat = comboBox1.SelectedIndex;  
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
