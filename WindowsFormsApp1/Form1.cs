using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public delegate void InvokeDelegate();
        System.Timers.Timer aTimer;
        System.Timers.Timer aTimer1;
        PictureBox[] cloud;
        int backgrounspeed;//скорость
        Random rnd;
        Bitmap bmp = new Bitmap(@"Z:\2020_2021 учебный год\41 ИС\Львов\анимация\1.png");
        int grad = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {   DoubleBuffered = true;
            InvokeDelegate invDel = oblako;
            invDel += SetTimeOblako;
            InvokeDelegate invDel3 = SetTimer;
            //BeginInvoke(new InvokeDelegate(oblako));
            //oblako();    
            //SetTimer();
        }
        // Вызов солнышка по таймеру
        private void timer1_Tick(object sender, EventArgs e)
        {
            Update_Pic();
        }
        // Вызов облаков по таймеру
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                //InvokeDelegate obl;
                //obl = toblak;
                //obl();
                toblak();
                Update_Pic1();
              
            } catch (Exception e1) { MessageBox.Show(e1.Message); }
          
        }

        // Таймер солнышка
        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(50);
            aTimer.Elapsed += timer1_Tick;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        // Таймер облоков
        private void SetTimeOblako()
        {
            aTimer1 = new System.Timers.Timer(100);
            aTimer1.Elapsed += timer2_Tick;
            aTimer1.AutoReset = true;
            aTimer1.Enabled = true;
        }
        // Вращение солнышка
        void RotateImage(ref Bitmap image, float angle)
        {
            try
            {
                using (Bitmap clone = (Bitmap)image.Clone())
                {
                    using (Graphics gbmp = Graphics.FromImage(clone))
                    {
                        gbmp.Clear(Color.Transparent);
                        gbmp.TranslateTransform(image.Width / 2f, image.Height / 2f);
                        gbmp.RotateTransform(angle);
                        gbmp.DrawImage(image, -image.Width / 2f, -image.Height / 2f);
                    }
                    image = (Bitmap)clone.Clone();
                }
                grad += (int)angle;
            }
            catch (Exception) { MessageBox.Show("лох"); }
        }
        // Обнавление облаков
        void Update_Pic1()
        {
            if ( InvokeRequired)
            {
              Invoke((MethodInvoker)(() =>
                { 
                    oblako();
                }));
            }
            else
            {
                oblako();
                SetTimeOblako();
            }
        }

        void Update_Pic()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { Pic_add();  }));  }
            else
            {
                Pic_add();
            }
            if (grad >= 360)
            {
                bmp = new Bitmap(@"Z:\2020_2021 учебный год\41 ИС\Львов\анимация\1.png");
                grad = 0;
            }
            oblako();
        }
        void Pic_add()
        {
            PictureBox pic = new PictureBox();
            pic.Name = "pictureBox1";
            RotateImage(ref bmp, 10);
            pic.Image = bmp;
            pic.Location = new Point(200, 100);
            pic.Size = new Size(200, 150);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Clear();
            this.Controls.Add(pic);
        }
        void toblak()
        {
            for (int i = 0; i < cloud.Length; i++)
            {
                cloud[i].Left += backgrounspeed;
                if (cloud[i].Left >= 1500)
                { cloud[i].Left = cloud[i].Height; }
            }
            for (int i = cloud.Length; i < cloud.Length; i++)
            {
                cloud[i].Left += backgrounspeed - 10;
                if (cloud[i].Left >= 1500)
                { cloud[i].Left = cloud[i].Left; }
            }
        }
        void oblako()
        { 
            //Инициализация объекта   
            backgrounspeed = 4;
            cloud = new PictureBox[15];
            rnd = new Random();
            for (int i = 0; i < cloud.Length; i++)
            {
                cloud[i] = new PictureBox();
                cloud[i].BorderStyle = BorderStyle.None;
                //Рандомное появлние на форме // 1 грани //ширин высота
                cloud[i].Location = new Point(rnd.Next(-1000, 1500), rnd.Next(60, 80));
                //Создаем 2 разновидности облаков
                if (i % 2 == 1)
                {//Рандомный размер(четные 1 размера, не четные другого)
                    cloud[i].Size = new Size(rnd.Next(100, 255), rnd.Next(20, 40));
                    //Рандомная позрачность
                    cloud[i].BackColor = Color.FromArgb(rnd.Next(50, 125), 255, 100, 100);
                }
                else
                {//длинна высота
                    cloud[i].Size = new Size(100,40);
                    cloud[i].BackColor = Color.FromArgb(rnd.Next(50, 125), 255, 205, 205);
                }
                this.Controls.Add(cloud[i]);
            }
  
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {

        }
    }
}
