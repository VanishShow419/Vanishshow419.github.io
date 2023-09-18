using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;


namespace OperationSystemCarTest
{
    public partial class Form1 : Form
    {
        private List<Bitmap> images = new List<Bitmap>(); // 用于存储图片的列表
        private int currentImageIndex = 0; // 当前图片索引
        // 新增的计时器用于图片切换
        private Timer imageSwitchTimer = new Timer();
        private DateTime pauseEndTime;
        private SoundPlayer soundPlayer;
        private bool musicPlaying = false;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            InitializeImages(); // 调用初始化图片的方法
            // 初始化新计时器
            imageSwitchTimer.Interval = 100; // 设置间隔为0.1秒
            imageSwitchTimer.Tick += new EventHandler(imageSwitchTimer_Tick);
            /*
             * 以绝对路径导入音乐时使用以下语句(例子)
             * string musicFilePath = @"C:\Users\user\Desktop\Study\School\Learning\操作系統\實驗\實驗1\operationSystemCarTest\OperationSystemCarTest\Resources\jntm.wav";
             * soundPlayer = new SoundPlayer(musicFilePath);
            */
            soundPlayer = new SoundPlayer(Properties.Resources.homo);
        }

        private void time_rePaint(object sender, EventArgs e)
        {
            //控制坤的移动
            int move_x = panel1.Size.Width / 162;
            int move_y = panel1.Size.Height / 192;
            int car_x = pictureBox1.Location.X + move_x;
            int car_y = pictureBox1.Location.Y + move_y;
            //判断坤是否超过窗体区域，超过则重新回到左上角
            if (car_x > this.Width - pictureBox1.Size.Width || car_y > this.Height - pictureBox1.Size.Height)
            {
                car_x = 0;
                car_y = 0;
            }
            //重新设置小坤的位置   
            pictureBox1.Location = new Point(car_x, car_y);
        }

        private void InitializeImages()
        {
            // 添加你的图片到 images 列表中
            images.Add(Properties.Resources.k1);
            images.Add(Properties.Resources.k2);
            images.Add(Properties.Resources.k3);
            images.Add(Properties.Resources.k4);
            images.Add(Properties.Resources.k5);
            images.Add(Properties.Resources.k6);
            images.Add(Properties.Resources.k7);
            images.Add(Properties.Resources.k8);
            images.Add(Properties.Resources.k3);
            images.Add(Properties.Resources.k2);
            images.Add(Properties.Resources.k1);

            // 设置初始图片
            pictureBox1.Image = images[currentImageIndex];
        }

        private void imageSwitchTimer_Tick(object sender, EventArgs e)
        {
            // 切换到下一张图片
            if (DateTime.Now >= pauseEndTime)
            {
                // 恢复计时器
                imageSwitchTimer.Start();
            }
            currentImageIndex++;
            if (currentImageIndex >= images.Count)
            {
                currentImageIndex = 0; // 循环显示图片
            }

            pictureBox1.Image = images[currentImageIndex];
        }

        private void button_start_click(object sender, EventArgs e)
        {
            
            /*
             * 以绝对路径的方式导入音乐时使用以下语句
             * soundPlayer.Play();
            */
            //音乐播放判断
            if (!musicPlaying)
            {
                soundPlayer.Play();
                musicPlaying = true;
            }
            //开启定时器，开始小坤的移动
            imageSwitchTimer.Start();
            timer1.Start();
        }

        private void button_stop_click(object sender, EventArgs e)
        {
            // 计算暂停结束的时间为当前时间加上114月
            pauseEndTime = DateTime.Now.AddMonths(114);
            //音乐播放判断
            if (musicPlaying)
            {
                soundPlayer.Stop();
                musicPlaying = false;
            }
            //暂停定时器、音乐播放器，暂停小坤的移动
            soundPlayer.Stop();
            imageSwitchTimer.Stop();
            timer1.Stop();
        }
        private void button_speedUp_click(object sender, EventArgs e)
        {
            //当刷新的时间间隔大于10ms
            if (timer1.Interval > 20)
            {
                timer1.Interval -= 10;
            }
            //当刷新的时间间隔大于5ms小于10ms
            else if (timer1.Interval > 5)
            {
                timer1.Interval -= 1;
            }
            else
            {
                timer1.Interval = 5;
            }
        }

        private void button_decelerate_click(object sender, EventArgs e)
        {
            //当刷新的时间间隔小于10ms
            if (timer1.Interval < 300)
            {
                timer1.Interval += 50;
            }
            else
            {
                timer1.Interval = 350;
            }
        }

        private void button_terminate_click(object sender, EventArgs e)
        {
            //退出程序
            System.Environment.Exit(0);
        }

        private void notifyIcon_mouseDoubleClick(object sender, MouseEventArgs e)
        {
            //显示本Winform程序
            this.Show();
        }

        private void toolStripMenuItem_restore(object sender, EventArgs e)
        {
            //显示本WinForm程序
            this.Show();
        }

        private void toolStripMenuItem_exit(object sender, EventArgs e)
        {
            //退出程序
            System.Environment.Exit(0);
        }

        private void form_formClosing(object sender, FormClosingEventArgs e)
        {
            //设置点击“X”后不会退出程序
            e.Cancel = true;
            //隐藏本WinForm程序
            this.Hide();
        }

        private void form_load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(0, 0);//设置初始位置为最左上角
        }

        private void form_mouseClick(object sender, MouseEventArgs e)
        {
            //显示本WinForm程序
            this.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // 设置图片框的初始位置和大小
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(300, 300); // 根据你的需求设置大小
        }
    }
}
