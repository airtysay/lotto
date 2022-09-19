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
            
        }

        private void btnThisClick(object sender, EventArgs e)
        {
            
            string conmStr = @"Data Source=C:\Users\Owner\Documents\GitHub\lotto2\lottodb.db";
            var con = new SQLiteConnection(conmStr);
                con.Open();
            
            for (int i = 0; i <= repeat; i++)
            {
                textBox1.Clear();
                string Number;
                Random random = new Random();
                int[] RandomArray = new int[6];
 
                 

                for (int j = 0; j < 6; j++) {
                    RandomArray[j] = random.Next(1, 45);
                    Delay(5);

                    for(int  x= 0; x<=j; x++)
                    {
                        if (RandomArray[j] == RandomArray[j - x])
                        {
                            RandomArray[j] = random.Next(1, 45);
                        }
                        else
                        {

                        }
                    }
                }
                for (int y = 0; y <= 6; y++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (RandomArray[k + 1] < RandomArray[k])
                        {
                            int q = RandomArray[k];
                            RandomArray[k] = RandomArray[k + 1];
                            RandomArray[k + 1] = q;

                        }
                    }
                }
                int bonus = random.Next(1, 45);
                for(int x = 0; x < 6; x++)
                {
                    if (RandomArray[x] == bonus)
                    {
                        bonus = random.Next(1, 45);
                    }
                    else
                    {

                    }
                }
                
                Number = String.Join(",", RandomArray);
                String sql = "insert into LottoNumber (LottoNumber) values('" + Number +"보너스 번호: " +bonus+ "')";
                text += Number + "보너스 번호: " + bonus + "\r\n";
                SQLiteCommand command = new SQLiteCommand(sql,con);
                Console.WriteLine(sql);
             
                int result = command.ExecuteNonQuery();
            }
            textBox1.Text = text;
            text = "";
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

       
        public void Delay(int ms)
        {
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime dateTimeAdd = dateTimeNow.Add(duration);
            while (dateTimeAdd >= dateTimeNow)
            {
                System.Windows.Forms.Application.DoEvents();
                dateTimeNow = DateTime.Now;
            }
            return;
        }
        
    }
}
