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
        int pick;
        int repeat;
        string text;
        Point fPt;
        bool isMove;

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



                for (int j = 0; j < 6; j++)
                {
                    RandomArray[j] = random.Next(1, 45);
                    Delay(5);

                    for (int x = 0; x <= j; x++)
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
                for (int x = 0; x < 6; x++)
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
                String sql = "insert into LottoNumber (LottoNumber) values('" + Number + "보너스 번호: " + bonus + "')";
                text += Number + "보너스 번호: " + bonus + "\r\n";
                SQLiteCommand command = new SQLiteCommand(sql, con);
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

        private void ListNum_Click(object sender, EventArgs e)
        {
            ListNum.Items.Clear();
            string conmStr = @"Data Source=C:\Users\Owner\Documents\GitHub\lotto2\lottodb.db";
            var con = new SQLiteConnection(conmStr);
            con.Open();
            String sql = "SELECT NUM FROM LottoNumber;";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader Num = command.ExecuteReader();

            while (Num.Read())
            {
                ListNum.Items.AddRange(new object[] { Num["Num"] });
            }
            Num.Close();
        }

        private void ListNyn_SelectedIndexChanged(object sender, EventArgs e)
        {
            pick = ListNum.SelectedIndex;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            string conmStr = @"Data Source=C:\Users\Owner\Documents\GitHub\lotto2\lottodb.db";
            var con = new SQLiteConnection(conmStr);
            con.Open();
            String sql = "SELECT * FROM LottoNumber WHERE Num = " + pick + ";";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader List = command.ExecuteReader();
            while (List.Read())
            {
                textBox1.Text = (string)List["LottoNumber"];
            }
            List.Close();  

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            int borderWidth = 100;
            Color borderColor = Color.Blue;

            /*ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, borderColor, borderWidth,
            ButtonBorderStyle.Solid, borderColor, borderWidth,
            ButtonBorderStyle.Solid, borderColor, borderWidth, 
            ButtonBorderStyle.Solid, borderColor, borderWidth, 
            ButtonBorderStyle.Solid);*/
        }

        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            isMove = true;
            fPt = new Point(e.X,e.Y);
            
        }

        private void pntTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove && (e.Button & MouseButtons.Left) == MouseButtons.Left)
                Location = new Point(this.Left-(fPt.X-e.X),this.Top - (fPt.Y -e.Y));
        }

        private void pntTop_MouseUp(object sender, MouseEventArgs e)
        {
            isMove=false;
        }
    }
}
