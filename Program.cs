using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace lotto
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Form fm = new Form();
            fm.Width = 500;
            fm.Height = 500;

            void fm_paint(object sender, PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                Pen pen = new Pen(Color.Black);

                g.DrawEllipse(pen, 500, 500, 500, 500);
            }
            fm.Paint += new PaintEventHandler(fm_paint);    
            왜안되냐
*/
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
