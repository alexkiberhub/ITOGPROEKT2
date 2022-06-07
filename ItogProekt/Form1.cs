using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItogProekt
{
    public partial class Form1 : Form
    {
        PointF p1;// первая точка
        PointF p2 = new PointF(1000, 400);// вторая точка
        Random R = new Random(); //для случайности
        double feta = 0;
        bool moveRight = false, moveLeft = false, launch = false; //переменные для проверки нажатия на кнопку
        float xe = 0F;
        float ye = 0F;
        float xp = 325;
        float yp = 375;
        int m = 0;
        int q = 0;
        double n = 0,launt=0;
        int i = 0;
        int c = 3;

        public Form1()
        {
            InitializeComponent();
            p1 = new PointF(this.Width / 2, this.Height - 100); //координаты первой точки
            Init();
        }

        public void Init()
        {
            
            timer1.Start();
            timer1.Tick += new EventHandler(Update);//для плавной анимации


        }

        public void Update(object sender, EventArgs e)
        {
            
            move();
            Invalidate(); //для плавной анимации
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart(); //перезапуск в случае нажатия соответственной кнопки

        }

        public void move()
        {
            if (launch && launt < 170) //алгоритм проверки попадания, засчета попадания
            {
                launt++;
                float alfa = float.Parse(Math.Atan2(-(yp - 25) + p2.Y, -(xp - 25) + p2.X).ToString());
                xp +=7* float.Parse(Math.Cos(alfa).ToString()); //перемещение по Х
                yp +=7* float.Parse(Math.Sin(alfa).ToString()); //перемещение по Y
                if (launt == 170)  //засчет промаха
                {

                    c--;
                }       
            }
            else
            {
                launt = 0;
                launch = false;
                xp = p1.X-25;
                yp = p1.Y-25;
            }
            if (xe <= 0) n = R.Next(3,9); //алгоритм передвижения "цели" 1 уровень
            else if (xe + 64 >= this.Width) n = R.Next(-9,-3);
            xe +=  float.Parse(n.ToString()); 
            ye = 0;
            if (i >= 5)// 2 уровень
            {
             ye = float.Parse((Math.Sin(xe / 50) * 80 + 80).ToString());
            }
            if (i >= 10)// 3 уровень
            {
                ye = float.Parse((Math.Cos(xe / 50) * 80 + 80).ToString());
            }
            if (i >= 15)// 4 уровень
            {
                ye = float.Parse((Math.Cos(xe / 25) * 90 + 90).ToString());
            }
            if (i >= 20)// 5 уровень
            {
                ye = float.Parse((Math.Cos(xe / 20) * 90 + 90).ToString());
            }
            if (i==25)// окончание игры
            {
                timer1.Stop();
                MessageBox.Show("Поздравляем вы прошли игру!");
                Application.Exit();

            }
            if (moveLeft && !moveRight ) feta -=3.14/240; //движение "траектории"
            else if (moveRight && !moveLeft) feta += 3.14 / 240; //движение "траектории"
            p2.X = 10000 * float.Parse(Math.Cos(feta).ToString());
            p2.Y = 10000 * float.Parse(Math.Sin(feta).ToString());

            if ((Math.Abs(xe-xp)<=32) && (Math.Abs(ye - yp) <= 32)) //коллизия
            {
                BackColor = Color.Pink;
                i++;
                c++;
                m = 1000;
                launt = 0;
                launch = false;
                xp = p1.X - 25;
                yp = p1.Y - 25;
            }
            if (c == 0)//алгоритм проигрыша
            {
                timer1.Stop();
                c = 0;
                i = 0;
                MessageBox.Show("Вы проиграли");
                
                button1.Visible = true;
                button1.Enabled = true;
            }
            if (m == 100)
            {
                if (q<100)
                {
                    label2.Visible = true;
                    label2.Enabled = true;
                    q++;
                    m = 110;

                }
                else if (m!=100)
                {
                    label2.Visible = false;
                    label2.Enabled = false;
                }

            }
            else if(q==10)
            {
                label2.Visible = false;
                label2.Enabled = false;
            }

            label1.Text = $"{i} Попаданий\r\n{c} В запасе";//статистика
        }



        private void Risuem(object sender, PaintEventArgs e)
        {
            //отрисовка шаров, траектории
            Graphics gr = e.Graphics;
            Pen p = new Pen(Color.Black, 2);// цвет линии и ширина
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (!launch)
            {
                gr.DrawLine(p, p1, p2);// рисуем линию
            }
            
            Pen p5 = new Pen(Color.Black, 3);


            // Create location and size of ellipse.

            float width = 50.0F;
            float height = 50.0F;

            // Create start and sweep angles.
            float startAngle = 0.0F;
            float sweepAngle = 360.0F;
            // Draw pie to screen.
            e.Graphics.DrawPie(p5, xe, ye, width, height, startAngle, sweepAngle);

            // Create solid brush.
            SolidBrush redBrush = new SolidBrush(Color.Red);
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            SolidBrush pinkBrush = new SolidBrush(Color.Pink);


            // Fill ellipse on screen.
            e.Graphics.FillEllipse(redBrush, xe, ye, width, height);

            // Draw pie to screen.
            e.Graphics.DrawPie(p5, xp, yp, width, height, startAngle, sweepAngle);
            // Fill ellipse on screen.
            e.Graphics.FillEllipse(greenBrush, xp, yp, width, height);



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //закрытие проекта
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //управление (стрелочки,пробел)
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
           
            if (e.KeyCode == Keys.Right)
            {
             moveRight = true;
            }
            if(e.KeyCode == Keys.Space)
            {
                //p1.Y += p2.Y;
                // p1.X += p2.X;

                launch = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
        }
    }
}
