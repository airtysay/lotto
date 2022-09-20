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
        int a;

        public Form1()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThisClick(object sender, EventArgs e)//로또 뽑기를 클릭시 실행
        {
            
            textBox1.Clear();
            //DB연동
            string conmStr = @"Data Source=C:\Users\Owner\Documents\GitHub\lotto2\lotto2db.db";
            var con = new SQLiteConnection(conmStr);
            con.Open();
                
            //Lotto 번호 생성
            Random random = new Random();
            int[] RandomArray = new int[7];// List 사용 안하고 Array 사용(크기가 고정)
            int lottoNum;
            //원하는 횟수 만큼 뽑기 가능
            for (int k = 0; k <= repeat; k++)
            {
                for (int i = 0; i < 7; i++)
                {
                    do
                    {
                        lottoNum = random.Next(1, 46);
                    }
                    while (RandomArray.Contains(lottoNum));
                    RandomArray[i] = lottoNum;
                }
                //insert 문 (축약시킬 방법을 모르겠음)
                String sql = "insert into LottoNumber (LottoNum1,LottoNum2,LottoNum3,LottoNum4,LottoNum5,LottoNum6,LottoNum7) values("
                    + RandomArray[0] + ","
                    + RandomArray[1] + ","
                    + RandomArray[2] + ","
                    + RandomArray[3] + ","
                    + RandomArray[4] + ","
                    + RandomArray[5] + ","
                    + RandomArray[6] + ")";

                SQLiteCommand command = new SQLiteCommand(sql, con);

                //로또 번호 출력
                Array.Sort(RandomArray,0,6);
                //색칠
                    for(int i = 0; i < 7; i++)
                {
                }
                    a = RandomArray[0];

                    switch (a)
                    {
                        case int a when (a < 10):
                            Graphics g = pictureBox1.CreateGraphics();
                            SolidBrush sb = new SolidBrush(Color.Yellow);
                            Rectangle r = new Rectangle(0, 0, 50, 50);
                            g.FillEllipse(sb, r);
                            g.Dispose();
                            break;
                        case int a when (a < 20):
                            g = pictureBox1.CreateGraphics();
                            sb = new SolidBrush(Color.Red);
                            r = new Rectangle(0, 0, 50, 50);
                            g.FillEllipse(sb, r);
                            g.Dispose();
                            break;
                        case int a when (a < 30):
                            g = pictureBox1.CreateGraphics();
                            sb = new SolidBrush(Color.Green);
                            r = new Rectangle(0, 0, 50, 50);
                            g.FillEllipse(sb, r);
                            g.Dispose();
                            break;
                        case int a when (a < 40):
                            g = pictureBox1.CreateGraphics();
                            sb = new SolidBrush(Color.Black);
                            r = new Rectangle(0, 0, 50, 50);
                            g.FillEllipse(sb, r);
                            g.Dispose();
                            break;
                        case int a when (a < 50):
                            g = pictureBox1.CreateGraphics();
                            sb = new SolidBrush(Color.SkyBlue);
                            r = new Rectangle(0, 0, 50, 50);
                            g.FillEllipse(sb, r);
                            g.Dispose();
                            break;
                    }
                
                
                text = "로또 번호; "
                    + RandomArray[0] + ","
                    + RandomArray[1] + ","
                    + RandomArray[2] + ","
                    + RandomArray[3] + ","
                    + RandomArray[4] + ","
                    + RandomArray[5] + ","
                    +
                    "보너스 번호: "
                    + RandomArray[6];

                textBox1.Text += text+"\r\n";

                Console.WriteLine(a);
                int result = command.ExecuteNonQuery();
            }
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
        //탑바 색조절
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            int borderWidth = 100;
            Color borderColor = Color.White;

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, borderColor, borderWidth,
            ButtonBorderStyle.Solid, borderColor, borderWidth,
            ButtonBorderStyle.Solid, borderColor, borderWidth, 
            ButtonBorderStyle.Solid, borderColor, borderWidth, 
            ButtonBorderStyle.Solid);
        }

        //탑바 이동관련
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
